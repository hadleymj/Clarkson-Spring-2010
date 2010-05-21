#include <ctype.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "jobs.h"

int isBackgroundJob(parseInfo * info)
{
	if(info->boolBackground)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

int addJob(char * curr_command, int childPid, jobList * jobs[])
{
	int i;
	for(i =0; i<MAX_JOB_NUM; ++i)
	{
		if(jobs[i] == (jobList *)0)
		{
			jobs[i] = (jobList *)malloc(sizeof(jobList));
			/* Here, command must be big enough to hold the output */
			jobs[i]->command = (char *)malloc(MAX_COMMAND_LENGTH*sizeof(char)); /* NOT sizeof ( 100 * char) */
			jobs[i]->PID = 0;
		        sprintf(jobs[i]->command, "%s", curr_command);	
			jobs[i]->PID = childPid;
			printf("[%d] %d\n",i,childPid);

			/* return the job number */
			return i;
		}
	}
	/* job queue full */
	return -1;
}

int removeJob(int job_num, jobList * jobs[], int kill_flag)
{
	if((job_num < MAX_JOB_NUM) && (job_num >= 0))
	{
		if(kill_flag)
			kill(jobs[job_num]->PID, SIGKILL);
		
		free(jobs[job_num]->command);
		free(jobs[job_num]); 
		jobs[job_num] = (jobList *)0;
	}
	else
	{
		printf("KeSH-1.0- job number %d doesn't exist\n", job_num);
	}
}

char *
getStatus(int stat)
{
	if(stat == 2)
		return "RUNNING";
	else if(stat == 1)
		return "STOPPED";
	else if(stat == 0)
		return "DONE";
	else
		return "Invalid Status";
}


void printJobs(jobList * jobs [])
{
	int i;
	for(i =0; i<MAX_JOB_NUM; ++i)
	{
		if((jobs[i] == (jobList *)0) || (jobs[i] == NULL))
			continue;
		else	
			printf ("[%d] %s: %d\n", i, jobs[i]->command, jobs[i]->PID);
	}
	return;
}

int
getJobNum(int pid, jobList * jobs [])
{
	int i;
	for(i = 0; i < MAX_JOB_NUM; i++)
	{
		if(jobs[i]->PID == pid)
			return i;
	}

	/* No current job with that pid */
	return -1;
}

int
numJobs(jobList * jobs [])
{
	int i;
	int count = 0;
	for(i =0; i<MAX_JOB_NUM; ++i)
	{
		if(jobs[i] != (jobList *)0)
		{
			count++;
		}
	}
	return count;
}
			


