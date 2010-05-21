#include <sys/types.h>
#include <unistd.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <fcntl.h>
#include <readline/readline.h>
#include <readline/history.h>
#include <assert.h>
#include "parse.h"
#include "userfs.h"
#include "crash.h"

/* GLOBAL  VARIABLES */
int virtual_disk;
superblock sb;  
BIT bit_map[BIT_MAP_SIZE];
dir_struct dir; /* represents the root directory */
dir_struct directory_listing[MAX_DIRECTORIES];
dir_struct * curr_dir;
inode curr_inode;
char buffer[BLOCK_SIZE_BYTES]; /* assert( sizeof(char) ==1)); */

/*
  man 2 read
  man stat
  man memcopy
*/


void usage (char * command) 
{
	fprintf(stderr, "Usage: %s -reformat disk_size_bytes file_name\n", 
		command);
	fprintf(stderr, "        %s file_ame\n", command);
}

char * buildPrompt()
{
	return  "%";
}


int main(int argc, char** argv)
{

	char * cmd_line;
	/* info stores all the information returned by parser */
	parseInfo *info; 
	/* stores cmd name and arg list for one command */
	struct commandType *cmd;

  
	init_crasher();

	if ((argc == 4) && (argv[1][1] == 'r'))
	{
		/* ./userfs -reformat diskSize fileName */
		if (!u_format(atoi(argv[2]), argv[3])){
			fprintf(stderr, "Unable to reformat\n");
			exit(-1);
		}
	}  else if (argc == 2)  {
   
		/* ./userfs fileName will attempt to recover a file. */
		if ((!recover_file_system(argv[1])) )
		{
			fprintf(stderr, "Unable to recover virtual file system from file: %s\n",
				argv[1]);
			exit(-1);
		}
	}  else  {
		usage(argv[0]);
		exit(-1);
	}
  
  
	/* before begin processing set clean_shutdown to FALSE */
	sb.clean_shutdown = 0;
	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &sb, sizeof(superblock));  
	sync();
	fprintf(stderr, "userfs available\n");

	while(1) { 

		cmd_line = readline(buildPrompt());
		if (cmd_line == NULL) {
			fprintf(stderr, "Unable to read command\n");
			continue;
		}

  
		/* calls the parser */
		info = parse(cmd_line);
		if (info == NULL){
			free(cmd_line); 
			continue;
		}

		/* com contains the info. of command before the first "|" */
		cmd=&info->CommArray[0];
		if ((cmd == NULL) || (cmd->command == NULL)){
			free_info(info); 
			free(cmd_line); 
			continue;
		}
  
		/************************   u_import ****************************/
		if (strncmp(cmd->command, "u_import", strlen("u_import")) ==0){

			if (cmd->VarNum != 3){
				fprintf(stderr, 
					"u_import externalFileName userfsFileName\n");
			} else {
				if (!u_import(cmd->VarList[1], 
					      cmd->VarList[2]) ){
					fprintf(stderr, 
						"Unable to import external file %s into userfs file %s\n",
						cmd->VarList[1], cmd->VarList[2]);
				}
			}
     

			/************************   u_export ****************************/
		} else if (strncmp(cmd->command, "u_export", strlen("u_export")) ==0){


			if (cmd->VarNum != 3){
				fprintf(stderr, 
					"u_export userfsFileName externalFileName \n");
			} else {
				if (!u_export(cmd->VarList[1], cmd->VarList[2]) ){
					fprintf(stderr, 
						"Unable to export userfs file %s to external file %s\n",
						cmd->VarList[1], cmd->VarList[2]);
				}
			}


			/************************   u_del ****************************/
		} else if (strncmp(cmd->command, "u_del", 
				   strlen("u_export")) ==0){
			
			if (cmd->VarNum != 2){
				fprintf(stderr, "u_del userfsFileName \n");
			} else {
				if (!u_del(cmd->VarList[1]) ){
					fprintf(stderr, 
						"Unable to delete userfs file %s\n",
						cmd->VarList[1]);
				}
			}


       
			/******************** u_ls **********************/
		} else if (strncmp(cmd->command, "u_ls", strlen("u_ls")) ==0){
			u_ls();


			/********************* u_quota *****************/
		} else if (strncmp(cmd->command, "u_quota", strlen("u_quota")) ==0){
			int free_blocks = u_quota();
			fprintf(stderr, "Free space: %d bytes %d blocks\n", 
				free_blocks* BLOCK_SIZE_BYTES, 
				free_blocks);

		/************************   u_pwd ****************************/
		} else if (strncmp(cmd->command, "u_pwd", 
				   strlen("u_export")) ==0){
			u_pwd();
		/************************   u_mkdir ****************************/
		} else if (strncmp(cmd->command, "u_mkdir", 
				   strlen("u_export")) ==0){
	
			if (cmd->VarNum != 2){
				fprintf(stderr, "u_mkdir userfsDirName\n");
			} else {
				if (!u_mkdir(cmd->VarList[1]) ){
					fprintf(stderr, 
						"Unable to make directory %s\n",
						cmd->VarList[1]);
				}
			}

		/************************   u_rmdir ****************************/
		} else if (strncmp(cmd->command, "u_rmdir", 
				   strlen("u_export")) ==0){
			if (cmd->VarNum != 2){
				fprintf(stderr, "u_rmdir userfsDirName\n");
			} else {
				if (!u_rmdir(cmd->VarList[1]) ){
					fprintf(stderr, 
						"Unable to remove directory %s\n",
						cmd->VarList[1]);
				}
			}
		/************************   u_cd_ ****************************/
		} else if (strncmp(cmd->command, "u_cd", 
				   strlen("u_export")) ==0){
			if (cmd->VarNum != 2){
				fprintf(stderr, "u_cd userfsDirName\n");
			} else {
				if (!u_cd(cmd->VarList[1]) ){
					fprintf(stderr, 
						"Unable to cd to directory %s\n",
						cmd->VarList[1]);
				}
			}



			/***************** exit ************************/
		} else if (strncmp(cmd->command, "exit", strlen("exit")) ==0){
			/* 
			 * take care of clean shut down so that u_fs
			 * recovers when started next.
			 */
			if (!u_clean_shutdown()){
				fprintf(stderr, "Shutdown failure, possible corruption of userfs\n");
			}
			exit(1);


			/****************** other ***********************/
		}else {
			fprintf(stderr, "Unknown command: %s\n", cmd->command);
			fprintf(stderr, "\tTry: u_import, u_export, u_ls, u_del, u_quota, exit\n");
		}

     
		free_info(info);
		free(cmd_line);
	}	      
  
}

/*
 * Initializes the bit map.
 */
void
init_bit_map()
{
	int i;
	for (i=0; i< BIT_MAP_SIZE; i++)
	{
		bit_map[i] = 0;
	}

}

void
allocate_block(int blockNum)
{
	assert(blockNum < BIT_MAP_SIZE);
	bit_map[blockNum]= 1;
}

void
free_block(int blockNum)
{
	assert(blockNum < BIT_MAP_SIZE);
	bit_map[blockNum]= 0;
}

int
superblockMatchesCode()
{
	if (sb.size_of_super_block != sizeof(superblock)){

		return 0;
	}
	if (sb.size_of_directory != sizeof (dir_struct)){
		return 0;
	}

	if (sb.size_of_inode != sizeof(inode)){
		return 0;
	}
	if (sb.block_size_bytes != BLOCK_SIZE_BYTES){
		return 0;
	}
	if (sb.max_file_name_size != MAX_FILE_NAME_SIZE){
		return 0;
	}
	if (sb.max_blocks_per_file != MAX_BLOCKS_PER_FILE){
		return 0;
	}
	return 1;
}

void
init_superblock(int diskSizeBytes)
{
	sb.disk_size_blocks  = diskSizeBytes/BLOCK_SIZE_BYTES;
	sb.num_free_blocks = u_quota();
	sb.clean_shutdown = 1;

	sb.size_of_super_block = sizeof(superblock);
	sb.size_of_directory = sizeof (dir_struct);
	sb.size_of_inode = sizeof(inode);

	sb.block_size_bytes = BLOCK_SIZE_BYTES;
	sb.max_file_name_size = MAX_FILE_NAME_SIZE;
	sb.max_blocks_per_file = MAX_BLOCKS_PER_FILE;
}

int 
compute_inode_loc(int inode_number)
{
	int whichInodeBlock;
	int whichInodeInBlock;
	int inodeLocation;

	whichInodeBlock = inode_number/INODES_PER_BLOCK;
	whichInodeInBlock = inode_number%INODES_PER_BLOCK;
  
	inodeLocation = (INODE_BLOCK + whichInodeBlock) *BLOCK_SIZE_BYTES +
		whichInodeInBlock*sizeof(inode);
  
	return inodeLocation;
}
int
write_inode(int inode_number, inode * in)
{

	int inodeLocation;
	assert(inode_number < MAX_INODES);

	inodeLocation = compute_inode_loc(inode_number);
  
	lseek(virtual_disk, inodeLocation, SEEK_SET);
	crash_write(virtual_disk, in, sizeof(inode));
  
	sync();

	return 1;
}


int
read_inode(int inode_number, inode * in)
{
	int inodeLocation;
	assert(inode_number < MAX_INODES);

	inodeLocation = compute_inode_loc(inode_number);

  
	lseek(virtual_disk, inodeLocation, SEEK_SET);
	read(virtual_disk, in, sizeof(inode));
  
	return 1;
}
	

/*
 * Initializes the directory.
 */
void
init_dir()
{
	/* Initialize all the directories at once */
	int i;
	int j;
	int k;

	dir.free = 0;
	dir.no_directories = 0;
	dir.parent_dir = NULL;
	for (i=0; i< MAX_FILES_PER_DIRECTORY; i++)
		dir.u_file[i].free = 1;

	for(i = 0; i < MAX_SUB_DIRECTORIES; i++)
		dir.u_dir[i] = NULL;

	curr_dir = &dir; 
	for(i = 0; i < MAX_DIRECTORIES; i++)
	{
		directory_listing[i].free = 1;
		directory_listing[i].parent_dir = NULL;
		for (j=0; j< MAX_FILES_PER_DIRECTORY; j++)
			directory_listing[i].u_file[j].free = 1;

		for(k = 0; k < MAX_SUB_DIRECTORIES; k++)
			directory_listing[i].u_dir[k] = NULL;

		
	}

	
}




/*
 * Returns the no of free blocks in the file system.
 */
int u_quota()
{

	int freeCount=0;
	int i;


	/* if you keep sb.num_free_blocks up to date can just
	   return that!!! */


	/* that code is not there currently so...... */

	/* calculate the no of free blocks */
	for (i=0; i < sb.disk_size_blocks; i++ )
	{

		/* right now we are using a full unsigned int
		   to represent each bit - we really should use
		   all the bits in there for more efficient storage */
		if (bit_map[i]==0)
		{
			freeCount++;
		}
	}
	return freeCount;
}

/* Find a free block in our bitmap */
int find_free_block()
{
	int k;
	for(k=3+NUM_INODE_BLOCKS+MAX_DIRECTORIES; k<BIT_MAP_SIZE; k++)
	{	
		if(bit_map[k]==0)
		{	
			allocate_block(k);
			return k;
		}
	}
	return -1;
}

int find_free_inode()
{
	int i = 0;
	inode in;
	int val = -1;
	for (i=0;i<MAX_INODES && val==-1;i++)
	{
		read_inode(i, &in);
		if (in.free)
		{
			val = i;
		}
	}
	return val;
}

int find_free_file(int dirBlock)
{
	int i = 0;
	int val = -1;
	dir_struct dirTmp;

	lseek(virtual_disk, BLOCK_SIZE_BYTES* dirBlock, SEEK_SET);
	read(virtual_disk, &dirTmp, sizeof(dir_struct));

	for (i=0;i<MAX_FILES_PER_DIRECTORY && val==-1;i++)
	{
		if (dirTmp.u_file[i].free)
			val = i;
	}
	return val;
}

/*
 * Imports a linux file into the u_fs
 * Need to take care in the order of modifying the data structures 
 * so that it can be revored consistently.
 */
int u_import(char* linux_file, char* u_file)
{
	int free_space;
	int inode_no = 1;
	struct stat fileInfo;
	int bytes_read;
	inode in;
	int blockIndex;
	int fd;

	free_space = u_quota();
	
	fd = open(linux_file,O_RDONLY);
	if ( -1 == fd ) 
	{
		printf("error, reading file %s\n",linux_file);
		return 0;
	}

	stat(linux_file, &fileInfo);
	if (fileInfo.st_size > free_space*BLOCK_SIZE_BYTES)
	{
		printf("Error: file size too big, not enough free space");
		return 0;
	}

	if ((inode_no = find_free_inode()) ==-1)
	{
		fprintf(stderr, "Error: no free inodes!!\n");
		return 0;
	}	

	sb.clean_shutdown = FALSE;
	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &sb, sizeof(superblock));
	sync();
	
	bytes_read = read(fd,buffer,BLOCK_SIZE_BYTES);
	blockIndex = 0;
	while (bytes_read > 0)
	{
		int block_no;
		if ((block_no = find_free_block()) ==-1)
		{
			fprintf(stderr, "Error: no free blocks?!\n");
			return 0;
		}	
		in.blocks[blockIndex++] = block_no*BLOCK_SIZE_BYTES;
		allocate_block(block_no);
		sb.num_free_blocks--;
		lseek(virtual_disk, BLOCK_SIZE_BYTES*block_no, SEEK_SET);
		crash_write(virtual_disk, buffer, bytes_read );

		bytes_read = read(fd,buffer,BLOCK_SIZE_BYTES);
	}

	if (bytes_read!=0)
	{
		fprintf(stderr, "Error: some random one!\n");
		return 0;
	}

	
	lseek(virtual_disk, BLOCK_SIZE_BYTES*BIT_MAP_BLOCK, SEEK_SET);
	crash_write(virtual_disk, bit_map, sizeof(BIT)*BIT_MAP_SIZE );
	sync();


	in.free = FALSE;
	in.file_size_bytes = fileInfo.st_size;
	in.no_blocks = blockIndex;

	write_inode(inode_no, &in);

	int fIndex = find_free_file(DIRECTORY_BLOCK);
	dir.u_file[fIndex].inode_number = inode_no;
	strcpy(dir.u_file[fIndex].file_name, u_file);
	dir.u_file[fIndex].free = 0;
	dir.no_files++;

	lseek(virtual_disk, BLOCK_SIZE_BYTES* DIRECTORY_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &dir, sizeof(dir_struct));
	sync();

	sb.clean_shutdown = TRUE;
	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &sb, sizeof(superblock));
	sync();




	/* write rest of code for importing the file.
	   return 1 for success, 0 for failure */


	/* here are some things to think about (not guaranteed to
	   be an exhaustive list !) */

	/* check you can open the file to be imported for reading
	   how big is it?? */

	/* check there is enough free space */

	/* check file name is short enough */

	/* check that file does not already exist - if it
	   does can just print a warning
	   could also delete the old and then import the new */

	/* check total file length is small enough to be
	   represented in MAX_BLOCKS_PER_FILE */

	/* check there is a free inode */

	/* check there is room in the directory */

	/* then update the structures: what all needs to be updates?  
	   bitmap, directory, inode, datablocks, superblock(?) */

	/* what order will you update them in? how will you detect 
	   a partial operation if it crashes part way through? */
 
	close(fd);	
	return 1;
}


/*
 * Exports a u_file to linux.
 * Need to take care in the order of modifying the data structures 
 * so that it can be revored consistently.
 */
int u_export(char* u_file, char* linux_file)
{
	int fd; /* file descriptor of the linux file that we are copying to */
	BOOLEAN file_found = 0;
	int i;
	inode fs_inode; /* inode of the file that we are exporting */

	if(open(linux_file, O_RDONLY) != -1) /* if a file already exists, die */
	{
		printf("the file already exists, omitting\n");
		return 0;
	}

	fd = open(linux_file,O_WRONLY | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR);

	if ( -1 == fd)
        {
                printf("error, opening linux system file handle %s\n",linux_file);
                return 0;
        }

	/* find the correct file in our virtual FS */
	for (i=0; i< MAX_FILES_PER_DIRECTORY ; i++)
	{
		if (!(dir.u_file[i].free) && (strcmp(dir.u_file[i].file_name, u_file) == 0))
		{
			file_found = 1;
			read_inode(dir.u_file[i].inode_number, &fs_inode);
			lseek(virtual_disk,fs_inode.blocks[0]*BLOCK_SIZE_BYTES, SEEK_SET);
			break;
		}
	}
	
	if(!file_found)
	{
		printf("error, file specified does not exist in the file system\n");
		return 0;
	}

	for(i = 0; i < fs_inode.no_blocks; i++)
	{
		read(virtual_disk,&buffer,BLOCK_SIZE_BYTES);
                write(fd, &buffer, BLOCK_SIZE_BYTES);
	}

	
	close(fd);

	return 1;
	/*
	  Wenjin said we dont need to delete the file we export
	
	  write code for exporting a file to linux.
	  x-return 1 for success, 0 for failure

	  x-check ok to open external file for writing

	  x-check userfs file exists

	  o-read the data out of ufs and write it into the external file
	*/


}


/*
 * Deletes the file from u_fs
 */
int u_del(char* u_file)
{
	BOOLEAN file_found = 0;
	int i;
	int j;
	inode fs_inode;
	/*
	  Write code for u_del.
	  return 1 for success, 0 for failure

	  check user fs file exists

	  update bitmap, inode, directory - in what order???

	  superblock only has to be up-to-date on clean shutdown?
	*/

	/* find the file in our virtual FS */
	for (i=0; i< MAX_FILES_PER_DIRECTORY ; i++)
	{
		if (!(dir.u_file[i].free) && (strcmp(dir.u_file[i].file_name, u_file) == 0))
		{
			file_found = 1;
			read_inode(dir.u_file[i].inode_number, &fs_inode);

			for(j=0; j < fs_inode.no_blocks; j++)
			{
				free_block(fs_inode.blocks[j]); 
				sb.num_free_blocks++;
			}			

			fs_inode.free = 1;
			dir.u_file[i].free = 1;
			dir.no_files--;

			break;
		}
	}
	
	if(!file_found)
	{
		printf("error, file specified does not exist in the file system\n");
		return 0;
	}


	return 1;
}

/*
 * Checks the file system for consistency.
 */
int u_fsck()
{
	int file_count = 0;
	int free_block_count = 0;
	BOOLEAN inode_flag = 0;
	BOOLEAN bitmap_flag = 0;
	int i;
	int j;
	int k;

	/* count the number of used files, ensure that this is equal to dir.no_files */
	for (i=0; i< MAX_FILES_PER_DIRECTORY ; i++)
        {
	   	if(!dir.u_file[i].free) /* find the files that are NOT free */
			++file_count;
	}

	if(file_count != dir.no_files)
	{
		printf("warning, the number of files counted is not equivalent to the directory\n");
		return 0;
	}


       /* loop through all the inodes to check two things;
	*
	*  1. Check that if an inode is marked not free, that it belongs to some file
	*  2. Ensure that the number of free blocks is equal to the superblock count.
	*
	*/ 

	
	for(j=0; j< MAX_INODES; j++)
	{
		read_inode(j, &curr_inode);
		if(curr_inode.free) /* if the inode is used, it must belong ot some file */
		{
			for (i=0; i< MAX_FILES_PER_DIRECTORY ; i++)
		        {
				/* If data integrity is maintained, there must be SOME file in the directory that has this inode number */
				if(dir.u_file[i].inode_number == j)
					inode_flag = 1;		  
        		}

			if(!inode_flag)
			{
				printf("warning, used inode found that does not correspond to any existing file, freeing inode\n");
				curr_inode.free = 1;
				return 0;
			}
	
		}

		/* loop through all of the blocks belong to this inode */
		for(k = 0; k<MAX_BLOCKS_PER_FILE; k++)
		{
			if(bit_map[curr_inode.blocks[k]] == 1)
				free_block_count++;
		}
	}

	if(sb.num_free_blocks != free_block_count)
	{
		printf("warning, the number of free blocks found in all of the inodes is not equivalent to the superblock\n");
		return 0;
	}
	
	/* Ensure that any used blocks in the bitmap are used by some file */

	for (i=3+NUM_INODE_BLOCKS+MAX_DIRECTORIES; i< BIT_MAP_SIZE; i++)
        {
	   	if(bit_map[i] == 1) /*If a block is allocated make sure that it is pointed to by some file */
			for (k=0; k< MAX_FILES_PER_DIRECTORY ; k++) 
			{	
				if(!dir.u_file[k].free) /* find the files that are NOT free */
				{
					read_inode(dir.u_file[k].inode_number, &curr_inode);
					for(j=0; j < curr_inode.no_blocks; j++) 
					{
						if(curr_inode.blocks[j] == i) 
							bitmap_flag = 1;
					}
				}
			}
	}
	
	if(!bitmap_flag)
	{
		printf("warning, there is a used block that is not pointed to by any existing file\n");
		return 0;
	}
	


	
	/*
	  Write code for u_fsck.
	  return 1 for success, 0 for failure

	  any inodes maked taken not pointed to by the directory?
	  
	  are there any things marked taken in bit map not
	  pointed to by a file?
	*/


	printf("File System integrity maintained!\n");
	return 1;
}

/* Multi-directory support functions */

int u_pwd()
{
	int i = 0;
	char  * directories[1024];
	dir_struct * temp_dir;
	temp_dir = curr_dir;
	while(temp_dir->parent_dir != NULL)
	{
		directories[i] = temp_dir->dir_name;
		temp_dir = curr_dir->parent_dir;
		i++;
	}

	directories[i] = "/";

	/* print them out in reverse order */
	while(i >= 0) {
		printf("%s", directories[i]);
		i--;
	}

	printf("\n");
	return 1;
}
/* int u_mkdir(char *)
 * Makes a new sub directory in the pwd 
 */
int u_mkdir(char * dirname)
{
	/* Should curr_dir be a pointer? */
	int i;
	if(curr_dir->no_directories >= MAX_SUB_DIRECTORIES)
	{
		printf("error, the maximum number of sub directories has been reached for the current directory\n");
		return 0;
	}

	if(countDirectories() > MAX_DIRECTORIES)
	{
		printf("error,the maximum number of directories has been reached for the file system\n");
		return 0;
	}
		
	/* find a free directory */
	for(i = 0; i < MAX_DIRECTORIES; i++)
	{
		if(directory_listing[i].free)
		{
			directory_listing[i].free = 0;
			directory_listing[i].parent_dir = curr_dir;
			strcpy(directory_listing[i].dir_name, dirname);
			curr_dir->no_directories++;
			curr_dir->u_dir[curr_dir->no_directories++] = &directory_listing[i];
			break;
		}
	}
			
	lseek(virtual_disk, BLOCK_SIZE_BYTES*(3+NUM_INODE_BLOCKS+i), SEEK_SET);
	crash_write(virtual_disk, curr_dir, sizeof(dir_struct));
	sync();

	return 1;
}

int countDirectories()
{
	int i;
	int count = 0;
	
	for(i = 0; i < MAX_SUB_DIRECTORIES; i++)
	{
		if(dir.u_dir[i] != NULL)
			count += countDirectorieshelper(&(dir.u_dir[i]));
	}
	return count;
}
int countDirectorieshelper(dir_struct * this_dir)
{
	int i;
	int count = 0;
	
	for(i = 0; i < MAX_SUB_DIRECTORIES; i++)
	{
		if(this_dir->u_dir[i] != NULL)
			count += countDirectorieshelper(&(this_dir->u_dir[i]));
	}
	return count;
}
		
/* int u_rmdir(char *)
 * Deletes the sub directory in the pwd
*/
int u_rmdir(char * dirname)
{
	int i;
	BOOLEAN dir_found = 0;
	for(i = 0; i < MAX_SUB_DIRECTORIES; i++)
	{
		if( (curr_dir->u_dir[i]  != NULL)&& strcmp(curr_dir->u_dir[i]->dir_name, dirname) == 0)
		{
			dir_found = 1;
			if(curr_dir->u_dir[i]->no_files != 0 || curr_dir->u_dir[i]->no_directories != 0)
			{
				/* unix-style, make sure that the directory is empty */
				printf("error, directory is not empty\n");
				return 0;
			}
			else
			{
				curr_dir->u_dir[i]->no_directories--;
				curr_dir->u_dir[i]->free = 1;
				curr_dir->u_dir[i] = NULL;
				
				break;
			}
		}
	}
	
	lseek(virtual_disk, BLOCK_SIZE_BYTES*(3+NUM_INODE_BLOCKS+i), SEEK_SET);
	crash_write(virtual_disk, curr_dir, sizeof(dir_struct));
	sync();
	return 1;
}

int searchSubDirectories(char * dirname)
{
	int i;
	for(i = 0; i < MAX_SUB_DIRECTORIES; i++)
	{
		if(curr_dir->u_dir[i] != NULL && strcmp(curr_dir->u_dir[i]->dir_name, dirname) == 0)
		{
			curr_dir = curr_dir->u_dir[i];
			return 0;
		}
	}
	return -1;
}
	
int u_cd(char * path)
{
	int i = 0;
	int j = 0;
	char * directories[1024];
	/* initialize the array */
	for(i = 0; i < 1024; i++)
	{
		directories[i] = (char *)malloc( sizeof(char)*MAX_FILE_NAME_SIZE);
	}

	i = 0;
	if(path == NULL)
	{
		printf("error, path does not exist\n");
		return 0;
	}
		
	if(path[0] == '/')
	{
		i = 1;
		curr_dir = &dir;
	}
		
	/* we are dealing with an absolute path */	
	for(; (i < MAX_FILE_NAME_SIZE) && path[i]; i++)
	{
		if(path[i] == '/')	
		{
			j++;
			continue;
		}
		directories[j][i] = path[i];		
	}
	
	directories[j][i] = '\0';

	for(i = 0; i <= j; i++)
	{
		if(directories[i] != NULL)
		{
			if(searchSubDirectories(directories[i]) == -1)
			{
				printf("error, path does not exist\n");
				return 0;
			}
		}
		else
			break;
	}
	

	printf("New directory name: %s\n", curr_dir->dir_name);
	return 1;
}


/*
 * Iterates through the directory and prints the 
 * file names, size and last modified date and time.
 */
void u_ls()
{
	int i;
	struct tm *loc_tm;
	int numFilesFound = 0;
	int numDirsFound = 0;

	for (i=0; i< MAX_FILES_PER_DIRECTORY ; i++)
	{
		if (!(curr_dir->u_file[i].free))
		{
			numFilesFound++;
			/* file_name size last_modified */
			
			read_inode(curr_dir->u_file[i].inode_number, &curr_inode);
			loc_tm = localtime(&curr_inode.last_modified);
			fprintf(stderr,"%s\t%d\t%d/%d\t%d:%d\n",curr_dir->u_file[i].file_name, 
				curr_inode.no_blocks*BLOCK_SIZE_BYTES, 
				loc_tm->tm_mon, loc_tm->tm_mday, loc_tm->tm_hour, loc_tm->tm_min);
      
		}  
	}

	for (i=0; i< MAX_SUB_DIRECTORIES; i++)
	{
		if (curr_dir->u_dir[i] != NULL && !(curr_dir->u_dir[i]->free))
		{
			numDirsFound++;
			
			fprintf(stderr,"%s\n",curr_dir->u_dir[i]->dir_name);
		}
	}

	if (numFilesFound == 0 && numDirsFound == 0){
		fprintf(stdout, "Directory empty\n");
	}

}

/*
 * Formats the virtual disk. Saves the superblock
 * bit map and the single level directory.
 */
int u_format(int diskSizeBytes, char* file_name)
{
	int i;
	int j;
	int minimumBlocks;

	/* create the virtual disk */
	if ((virtual_disk = open(file_name, O_CREAT|O_RDWR, S_IRUSR|S_IWUSR)) < 0)
	{
		fprintf(stderr, "Unable to create virtual disk file: %s\n", file_name);
		return 0;
	}


	fprintf(stderr, "Formatting userfs of size %d bytes with %d block size in file %s\n",
		diskSizeBytes, BLOCK_SIZE_BYTES, file_name);

	minimumBlocks = 3+ NUM_INODE_BLOCKS+1;
	if (diskSizeBytes/BLOCK_SIZE_BYTES < minimumBlocks){
		/* 
		 *  if can't have superblock, bitmap, directory, inodes 
		 *  and at least one datablock then no point
		 */
		fprintf(stderr, "Minimum size virtual disk is %d bytes %d blocks\n",
			BLOCK_SIZE_BYTES*minimumBlocks, minimumBlocks);
		fprintf(stderr, "Requested virtual disk size %d bytes results in %d bytes %d blocks of usable space\n",
			diskSizeBytes, BLOCK_SIZE_BYTES*minimumBlocks, minimumBlocks);
		return 0;
	}


	/*************************  BIT MAP **************************/

	assert(sizeof(BIT)* BIT_MAP_SIZE <= BLOCK_SIZE_BYTES);
	fprintf(stderr, "%d blocks %d bytes reserved for bitmap (%d bytes required)\n", 
		1, BLOCK_SIZE_BYTES, sizeof(BIT)* BIT_MAP_SIZE );
	fprintf(stderr, "\tImplies Max size of disk is %d blocks or %d bytes\n",
		BIT_MAP_SIZE, BIT_MAP_SIZE*BLOCK_SIZE_BYTES);
  
	if (diskSizeBytes >= BIT_MAP_SIZE* BLOCK_SIZE_BYTES){
		fprintf(stderr, "Unable to format a userfs of size %d bytes\n",
			diskSizeBytes);
		return 0;
	}

	init_bit_map();
  
	/* first three blocks will be taken with the 
	   superblock, bitmap and directory */
	allocate_block(BIT_MAP_BLOCK);
	allocate_block(SUPERBLOCK_BLOCK);
	allocate_block(DIRECTORY_BLOCK);
	/* next NUM_INODE_BLOCKS will contain inodes */
	for (i=3; i< 3+NUM_INODE_BLOCKS; i++){
		allocate_block(i);
	}

	/* next MAX_DIRECTORIES blocks will contain other directories*/
	for (i=3+NUM_INODE_BLOCKS; i< 3+NUM_INODE_BLOCKS+MAX_DIRECTORIES; i++){
		allocate_block(i);
	}
  
	lseek(virtual_disk, BLOCK_SIZE_BYTES*BIT_MAP_BLOCK, SEEK_SET);
	crash_write(virtual_disk, bit_map, sizeof(BIT)*BIT_MAP_SIZE );



	/***********************  DIRECTORY  ***********************/
	assert(sizeof(dir_struct) <= BLOCK_SIZE_BYTES);

	fprintf(stderr, "%d blocks %d bytes reserved for the userfs directory (%d bytes required)\n", 
		1, BLOCK_SIZE_BYTES, sizeof(dir_struct));
	fprintf(stderr, "\tMax files per directory: %d\n",
		MAX_FILES_PER_DIRECTORY);
	fprintf(stderr, "\tMax subdirectories per directory: %d\n",
		MAX_SUB_DIRECTORIES);
	fprintf(stderr,"Directory entries limit filesize to %d characters\n",
		MAX_FILE_NAME_SIZE);

	init_dir(); /* initializes all of the possible directories  */
	
	lseek(virtual_disk, BLOCK_SIZE_BYTES* DIRECTORY_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &dir, sizeof(dir_struct));

	j = 0;
	for(i = 3+NUM_INODE_BLOCKS; i <= 3+NUM_INODE_BLOCKS + MAX_DIRECTORIES; i++)
	{
		lseek(virtual_disk, BLOCK_SIZE_BYTES* i, SEEK_SET);
		crash_write(virtual_disk, &directory_listing[j], sizeof(dir_struct));
		j++;
	}

	/***********************  INODES ***********************/
	fprintf(stderr, "userfs will contain %d inodes (directory limited to %d)\n",
		MAX_INODES, MAX_FILES_PER_DIRECTORY);
	fprintf(stderr,"Inodes limit filesize to %d blocks or %d bytes\n",
		MAX_BLOCKS_PER_FILE, 
		MAX_BLOCKS_PER_FILE* BLOCK_SIZE_BYTES);

	curr_inode.free = 1;
	for (i=0; i< MAX_INODES; i++){
		write_inode(i, &curr_inode);
	}

	/***********************  SUPERBLOCK ***********************/
	assert(sizeof(superblock) <= BLOCK_SIZE_BYTES);
	fprintf(stderr, "%d blocks %d bytes reserved for superblock (%d bytes required)\n", 
		1, BLOCK_SIZE_BYTES, sizeof(superblock));
	init_superblock(diskSizeBytes);
	fprintf(stderr, "userfs will contain %d total blocks: %d free for data\n",
		sb.disk_size_blocks, sb.num_free_blocks);
	fprintf(stderr, "userfs contains %d free inodes\n", MAX_INODES);
	  
	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &sb, sizeof(superblock));
	sync();


	/* when format complete there better be at 
	   least one free data block */
	assert( u_quota() >= 1);
	fprintf(stderr,"Format complete!\n");

	return 1;
} 

/*
 * Attempts to recover a file system given the virtual disk name
 */
int recover_file_system(char *file_name)
{

	if ((virtual_disk = open(file_name, O_RDWR)) < 0)
	{
		printf("virtual disk open error\n");
		return 0;
	}

	/* read the superblock */
	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	read(virtual_disk, &sb, sizeof(superblock));

	/* read the bit_map */
	lseek(virtual_disk, BLOCK_SIZE_BYTES*BIT_MAP_BLOCK, SEEK_SET);
	read(virtual_disk, bit_map, sizeof(BIT)*BIT_MAP_SIZE );

	/* read the single level directory */
	lseek(virtual_disk, BLOCK_SIZE_BYTES* DIRECTORY_BLOCK, SEEK_SET);
	read(virtual_disk, &dir, sizeof(dir_struct));

	if (!superblockMatchesCode()){
		fprintf(stderr,"Unable to recover: userfs appears to have been formatted with another code version\n");
		return 0;
	}
	if (!sb.clean_shutdown)
	{
		/* Try to recover your file system */
		fprintf(stderr, "u_fsck in progress......");
		if (u_fsck()){
			fprintf(stderr, "Recovery complete\n");
			return 1;
		}else {
			fprintf(stderr, "Recovery failed\n");
			return 0;
		}
	}
	else{
		fprintf(stderr, "Clean shutdown detected\n");
		return 1;
	}
}


int u_clean_shutdown()
{
	/* write code for cleanly shutting down the file system
	   return 1 for success, 0 for failure */
  
	sb.num_free_blocks = u_quota();
	sb.clean_shutdown = 0;

	lseek(virtual_disk, BLOCK_SIZE_BYTES* SUPERBLOCK_BLOCK, SEEK_SET);
	crash_write(virtual_disk, &sb, sizeof(superblock));
	sync();

	close(virtual_disk);
	/* is this all that needs to be done on clean shutdown? */
	return !sb.clean_shutdown;
}
