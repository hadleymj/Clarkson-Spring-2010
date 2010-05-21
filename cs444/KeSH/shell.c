/*
   KeSH 1.0
   Mike Hadley and James Licata
   CS444 Lab Project 1
*/
#include <stdio.h>
#include <stdlib.h>
#include <strings.h>
#include <math.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/wait.h>
#include <readline/readline.h>
#include <readline/history.h>
#include "jobs.h"

#define DEBUG 0

enum
BUILTIN_COMMANDS { NO_SUCH_BUILTIN=0, EXIT,JOBS,KILL,CD,HISTORY,HELP};

char *
buildPrompt()
{
  size_t size = 256;
  char * buffer = (char *)malloc(size);
  getcwd(buffer, size);
  strcat(buffer, "-$");
  return  buffer;
}

void
printPrompt(char * prompt)
{
	printf("%s", prompt);
	return;
}

int
isBuiltInCommand(char * cmd){

  if ( strncmp(cmd, "exit", strlen("exit")) == 0){
    return EXIT;
  }
  else if ( strncmp(cmd, "history", strlen("history")) == 0){
    return HISTORY;
  }
  else if ( strncmp(cmd, "cd", strlen("cd")) == 0){
    return CD;
  }
  else if ( strncmp(cmd, "kill", strlen("kill")) == 0){
    return KILL;
  }
  else if ( strncmp(cmd, "jobs", strlen("jobs")) == 0){
    return JOBS;
  }
  else if ( strncmp(cmd, "help", strlen("help")) == 0){
    return HELP;
  }
  else {
  	return NO_SUCH_BUILTIN;
  }
}

/* process_IO takes a input filename and a io_flag that specifies whether
 * this file is for input or output */
int
process_io_redir(char * file, int io_flag){
	/* Here, args is a pointer to an array of strings*/
	int fdin;
	int fdout;
	if(io_flag)
	{
		/*INPUT*/
		if((fdin = open(file, O_RDONLY, 0)) == -1)
		{
			return -1;
		}
		else
		{
			dup2(fdin, 0);
		}
	}
	else
	{
		/*OUTPUT*/
		if((fdout = open(file, O_WRONLY, 0)) == -1)
		{
			creat(file, 0644);
			fdout = open(file, O_WRONLY, 0);
			dup2(fdout, 1);
		}
		else
		{
			dup2(fdout, 1);
		}
	}
	return 0;
}

int
executeCommand(char * argv[]){
	return execvp(argv[0], argv);
}

char *
concat_VarList(char * list [], char * command, char * buf_command)
{
	int i;
	/* Use sprintf here to get the firs command in VarList */
	/* strcat adds an arbitrary space at the beginning... */
	sprintf(buf_command, "%s", command);
	for(i = 1; i < MAX_VAR_NUM; i++)
	{
		if(list[i] != NULL)
		{
			strcat(buf_command, " ");
			strcat(buf_command, list[i]);
		}
		else
			break;
	}
	return buf_command;
}
		
int
callExternalCommand(parseInfo * info, struct commandType * com, jobList * jobs[])
{
     int i;
     int io_redir_err = 0;
     int childPid;
     int fd[2];
     char readbuffer[80]; /*Allow for filenames that are up to 80 characters in length */
     char readbuffer2[4096]; /*Allow for filenames that are up to 80 characters in length */
    
     /* popen file streams; used for pipes*/
     FILE * output;
     FILE * input;

     /* Full command strings; used for pipes */
     char full_command_1[MAX_COMMAND_LENGTH];
     char full_command_2[MAX_COMMAND_LENGTH];

     if(info->pipeNum)
     { 
	     FILE * pipe_file_1 = fopen("pipefile1", "w");
	     FILE * pipe_file_2 = fopen("pipefile2", "w");
	     FILE * pipe_file_3 = fopen("pipefile3", "w");

	      /* Deal with the pipes using the popen API */
	     for(i = 0; i <= info->pipeNum; i++)
	     {
		/* We have pipes to deal with, use the pipe() system call and then fork */
		concat_VarList((&info->CommArray[i])->VarList,(&info->CommArray[i])->command,full_command_1);
		concat_VarList((&info->CommArray[i+1])->VarList,(&info->CommArray[i+1])->command,full_command_2);

		if(i == info->pipeNum) /* The last command, check to see if there is file redirection */
		{
			/* Just modify the command to pass into popen. Don't dup the file descriptors, it won't work out
			  with popen. You need to dup after you fork and before you execvp, but popen does all of that. */

			     if(info->boolOutfile)
			     {
				strcat(full_command_2, " > ");
				strcat(full_command_2, info->outFile);
			     }

			     if(info->boolInfile)
			     {
				strcat(full_command_2, " < ");
				strcat(full_command_2, info->inFile);
			     }
		}
		else
		{
			strcat(full_command_1, " > ");
			strcat(full_command_1, "pipefile1");

			strcat(full_command_2, " > ");
			strcat(full_command_2, "pipefile2");
		}


		if(i != 0)
		{
			while(fgets(readbuffer, sizeof(readbuffer), pipe_file_2 ))
			{
				fputs(readbuffer, pipe_file_3);
			}
		}
	
		output = (FILE *)popen(full_command_1, "w");
		input = (FILE *)popen(full_command_2, "w");
			

		if(!output)
		{
			printf("Could not run command: %s\n",(&info->CommArray[i])->command); 
			return 1;
		}

		/* use fgets instead of read
	 *    	   fgets uses the FILE * directly, while read uses the file descriptor */
		if(i == info->pipeNum) /* If we're at the last command, we don't have to worry about anymore piping, just break */
		{

			pclose(output);
/*
			free(full_command_1);
			free(full_command_2);
			if(i != 0)
			{
				free(readbuffer);
				free(readbuffer2);
			}
			*/
			break;
		}
		
		if(i != 0)
		{
			while(fgets(readbuffer, sizeof(readbuffer), pipe_file_2 ))
			{
				fputs(readbuffer, pipe_file_3);
			}
		}

		while(fgets(readbuffer, sizeof(readbuffer), pipe_file_1))
		{
			fputs(readbuffer, input);
		}
	     }
	return 0;
     }	
     else
     {	

	     /* If we don't have any pipes to deal with, just fork a new process */
	     childPid = fork();

			
	     if (childPid == 0){
	/* Child Process, which at this point is just a clone of its parent */
	/* Call process_IO to change file descriptions BEFORE you execvp and run the appropriate process image (for some obscure reason*/
     /* if we have some I/O redirection, dup the file descriptors and then execvp */

		    if( info->boolOutfile || info->boolInfile) 
		    {
			     if(info->boolOutfile)
			     {
				/* use the process_IO function to change the file descriptors */
				io_redir_err = process_io_redir(info->outFile, 0); /* set the flag as 1 for input, 0 for output */
			     }

			     if(info->boolInfile)
			     {
				io_redir_err = process_io_redir(info->inFile, 1); 
			     }

			     if(io_redir_err == -1)
			     {
				printf("KeSH-1.0- no such file or directory '%s'", info->inFile);	
				/* don't bother running the command */
				exit(1);
			     }
		    }
		     executeCommand(com->VarList);
	     } 
	     else 
	     {
		if (isBackgroundJob(info)){
			addJob(com->command, childPid, jobs); 
			return 0;
		} 
		else 
		{ 
			waitpid(childPid, NULL, 0);
			return 0; 
		}		
    	    }
	}
}


int
main (int argc, char **argv)
{
  int i;
  int j;

  int childPid;
  HIST_ENTRY **he_arr;
  char *expansion;
  int result;
  char * cmdLine;
  char * prompt;
  parseInfo *info; /*info stores all the information returned by parser.*/
  struct commandType *com; /*com stores command name and Arg list for one command.*/
  int fildes[2]  = {1,0};
  char readbuffer[80];
  int return_external; 
  int jobID;
  int PID;

  int bg_status[MAX_JOB_NUM]; /* stores the background job status for a specific job */
  jobList * jobs[MAX_JOB_NUM]; /*stores background jobs*/

  for(i = 0; i < MAX_JOB_NUM; i++)
  {
	jobs[i] = (jobList *)0;
	/* initialize all of the background statuses to 1 */
	bg_status[i] = 1;
  }

#ifdef UNIX

    fprintf(stdout, "This is the UNIX version\n");
#endif

#ifdef WINDOWS
    fprintf(stdout, "This is the WINDOWS version\n");
#endif

  using_history();
  while(1){

  cmdLine = (char *)NULL;
#ifdef UNIX
    /*Work in functionality to NOT print the prompt again if the previous
      command erquires input from standard in i.e. "cat" */
    
    prompt = buildPrompt();
    cmdLine = readline(prompt);
    if (cmdLine == (char *)NULL) {
      fprintf(stderr, "Unable to read command\n");
      continue;
    }
    if (!(cmdLine && *cmdLine))
    {
	continue;
    }
#endif
/* use the GNU readline history expand feature to keep track of past commands */
 result = history_expand (cmdLine, &expansion);
 if (result)
 	fprintf (stderr, "%s\n", expansion);

 if (result < 0 || result == 2)
 {
 	free (expansion);
 	continue;
 }

 add_history (expansion);
 strcpy (cmdLine, expansion); 
 free (expansion);
    /*calls the parser*/
    info = parse(cmdLine);
    if (info == NULL){
      free(cmdLine);
      continue;
    }
#if DEBUG
    /*prints the info struct*/
    print_info(info);
#endif

    /*com contains the info. of the command before the first "|"*/
    /* HERE, -> has HIGHER precedence than the &!!! info->CommArray[0] returns a struct commandType */
    com=&info->CommArray[0];
    if ((com == NULL)  || (com->command == NULL)) {
      free_info(info);
      free(cmdLine);
      continue;
    }
    /*com->command tells the command name of com*/
    if (isBuiltInCommand(com->command) == EXIT){
      if(numJobs(jobs))
	printf("KeSH-1.0- cannot exit, %d background jobs remain\n", numJobs(jobs));
      else
      	exit(1);
    }
    else if (isBuiltInCommand(com->command) == CD){
	const char * filename = (const char *)com->VarList[1];
#if DEBUG
	printf("CD Filename: %s\n", filename);
#endif
        if(chdir(filename) == 0)
	{
#if DEBUG
		printf("Changing Directory...\n");
#endif
	}
	else
	{
		printf("KeSH-1.0- failed to change directory\n");
	}
    }
    else if (isBuiltInCommand(com->command) == HISTORY){
	he_arr = history_list();
	if(he_arr)
	{
		i = history_length - 10;
		if(i > 0)
		{
			for(j = 9; (he_arr[i] && j >= 0); j--){ /* note that we don't use sizeof here */
			  printf ("%d: %s\n", history_length - j, he_arr[i]->line);
			  i++;
			}
		}
		else
		{
			i = 0;
			while(he_arr[i]){ /* note that we don't use sizeof here */
			  printf ("%d: %s\n", i+1, he_arr[i]->line);
			  i++;
			}
			
		}
	}
    }
   else if (isBuiltInCommand(com->command) == JOBS)
   {
	/* Update the job list before printing */
	int i;
	for(i = 0; i < MAX_JOB_NUM; i++)
	{
		/* If the process has exited successfully, remove it from the running background jobs list */
		if(jobs[i])
		{
			waitpid (jobs[i]->PID, (bg_status + i), WNOHANG); 
			if(WIFEXITED(bg_status[i]))
				removeJob(i, jobs, 0);				
		}
	}				
	printJobs(jobs);
   }

    else if (isBuiltInCommand(com->command) == KILL)
    {
	jobID =atoi(com->VarList[1]);
	removeJob(jobID, jobs, 1);
    }

    else if (isBuiltInCommand(com->command) == HELP){
	printf("\nKeSH 1.0 By Mike Hadley and James Licata \n\nThe Following Builtin Commands are supported: \n\n");
	printf("Change Directory (cd)\n \t cd <dir>\n");
	printf("List Command History (history)\n \t history\n");
	printf("List Background Processes (jobs)\n \t jobs\n");
	printf("Kill a Process (kill)\n \t kill <process id>\n");
	printf("Help on Commands (help)\n \t help\n");
	printf("Exit the Shell (exit)\n \t exit\n");
    }
    else 
    {
		if(isBackgroundJob(info))
			callExternalCommand(info,com,jobs);
		else
			callExternalCommand(info,com,jobs);
    }

    free_info(info);
    free(cmdLine);
    free(prompt);
  }/* while(1) */
}






