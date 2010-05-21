#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h>

#ifdef UNIX
#include <pthread.h>
#endif

#include <errno.h>

#ifdef WINDOWS
#include <Windows.h>
#endif

#define MAXGRIDSIZE 	10
#define MAXTHREADS	1000
#define NO_SWAPS	20

extern int errno;

typedef enum {GRID, ROW, CELL, NONE} grain_type;
int gridsize = 0;
int grid[MAXGRIDSIZE][MAXGRIDSIZE];
int threads_left = 0;

#ifdef WINDOWS
typedef struct
{
  DWORD id;
  char *str;
  grain_type *g_type;
}arguments;
#endif


#ifdef UNIX
pthread_mutex_t m_nthreads;
pthread_mutex_t m_grid;
pthread_mutex_t m_row[MAXGRIDSIZE];
pthread_mutex_t m_cell[MAXGRIDSIZE*MAXGRIDSIZE];
#endif

#ifdef WINDOWS
HANDLE m_nthreads;
HANDLE m_grid;
HANDLE m_row[MAXGRIDSIZE];
HANDLE m_cell[MAXGRIDSIZE*MAXGRIDSIZE];
#endif

time_t start_t, end_t;

int PrintGrid(int grid[MAXGRIDSIZE][MAXGRIDSIZE], int gridsize)
{
	int i;
	int j;
	
	for (i = 0; i < gridsize; i++)
	{
		for (j = 0; j < gridsize; j++)
			fprintf(stdout, "%d\t", grid[i][j]);
		fprintf(stdout, "\n");
	}
	return 0;
}

#ifdef WINDOWS
DWORD WINAPI myThread(LPVOID Param)
{
    arguments *arg = (arguments *)Param;
    //printf("This thread is %u %s\n", arg->id, arg->str );
    return 0;
}
#endif

long InitGrid(int grid[MAXGRIDSIZE][MAXGRIDSIZE], int gridsize)
{
	int i;
	int j;
	long sum = 0;
	int temp = 0;

	srand( (unsigned int)time( NULL ) );


	for (i = 0; i < gridsize; i++)
		for (j = 0; j < gridsize; j++) {
			temp = rand() % 100;			
			grid[i][j] = temp;
			sum = sum + temp;
		}

	/* Thread number mutex initialization */
	#ifdef UNIX
	pthread_mutex_init(&m_nthreads, NULL);
	#endif

	#ifdef WINDOWS
	m_nthreads=CreateMutex(NULL, FALSE, NULL);
	#endif

	/* Grid mutex intialization */
	#ifdef UNIX
	pthread_mutex_init(&m_grid, NULL);
	#endif

	#ifdef WINDOWS
	m_grid=CreateMutex(NULL, FALSE, NULL);
	#endif

	/* Row mutex intializations */
	for(i = 0; i < gridsize; i++)
	{
		#ifdef UNIX
		pthread_mutex_init(&m_row[i], NULL);
		#endif

		#ifdef WINDOWS
		m_row[i]=CreateMutex(NULL, FALSE, NULL);
		#endif
	}

	/* Cell mutex intializations */
	for(i = 0; i < (gridsize*gridsize); i++)
	{
		#ifdef UNIX
		pthread_mutex_init(&m_cell[i], NULL);
		#endif

		#ifdef WINDOWS
		m_cell[i]=CreateMutex(NULL, FALSE, NULL);
		#endif
	}
	return sum;

}

long SumGrid(int grid[MAXGRIDSIZE][MAXGRIDSIZE], int gridsize)
{
	int i;
	int j;
	long sum = 0;


	for (i = 0; i < gridsize; i++){
		for (j = 0; j < gridsize; j++) {
			sum = sum + grid[i][j];
		}
	}
	return sum;

}
void grid_lock()
{
	#ifdef UNIX
	pthread_mutex_lock(&m_grid);
	#endif

	#ifdef WINDOWS
	WaitForSingleObject(m_grid, INFINITE);
	#endif

	return;
}

void grid_unlock()
{
	#ifdef UNIX
	pthread_mutex_unlock(&m_grid);
	#endif

	#ifdef WINDOWS
	ReleaseMutex(m_grid);
	#endif

	return;
}

void row_lock(int r1)
{
	#ifdef UNIX
	pthread_mutex_lock(&m_row[r1]);
	#endif

	#ifdef WINDOWS
	WaitForSingleObject(m_row[r1], INFINITE);
	#endif

	return;
}

void row_unlock(int r1)
{
	#ifdef UNIX
	pthread_mutex_unlock(&m_row[r1]);
	#endif

	#ifdef WINDOWS
	ReleaseMutex(m_row[r1]);
	#endif

	return;
}

void cell_lock(int r1, int c1)
{
	#ifdef UNIX
	pthread_mutex_lock(&m_cell[(c1*r1) + c1]);
	#endif

	#ifdef WINDOWS
	WaitForSingleObject(m_cell[(c1*r1)], INFINITE);
	#endif

	return;
}

void cell_unlock(int r1, int c1)
{
	#ifdef UNIX
	pthread_mutex_unlock(&m_cell[(c1*r1) + c1]);
	#endif

	#ifdef WINDOWS
	ReleaseMutex(m_cell[(c1*r1)]);
	#endif

	return;
}

#ifdef UNIX
void* do_swaps(void* args)
#endif
#ifdef WINDOWS
DWORD WINAPI do_swaps(LPVOID Param)
#endif
{

	int i, row1, column1, row2, column2;
	int temp;
	
	#ifdef UNIX
	grain_type* gran_type = (grain_type*)args;
	pthread_mutex_lock(&m_nthreads);
	#endif

	#ifdef WINDOWS
	arguments *c_args = (arguments *)Param;
	grain_type* gran_type = c_args->g_type;
	WaitForSingleObject(m_nthreads, INFINITE);
	#endif

	threads_left++;

	#ifdef UNIX
	pthread_mutex_unlock(&m_nthreads);
	#endif

	#ifdef WINDOWS
	ReleaseMutex(m_nthreads);
	#endif

	
	for(i=0; i<NO_SWAPS; i++)
	{
		row1 = rand() % gridsize;
		column1 = rand() % gridsize;	
		row2 = rand() % gridsize;
		column2 = rand() % gridsize;


		/* Make sure that we're not trying to swap the same cell */
		while((row1 == row2) && (column1 == column2))
		{
			row2 = rand() % gridsize;
			column2 = rand() % gridsize;
		}
			


		if (*gran_type == ROW)
		{
		  /* obtain row level locks*/
		  /* Prevent circular waiting */
		 if(row1 > row2)
		 {
		 	row_lock(row2);
		 	row_lock(row1);
		 }
		 else
		 {
		 	row_lock(row1);
			if (row1 != row2) /* Check to make sure that we aren't trying to lock the same mutex again */
		 		row_lock(row2);
		 }

		}
		else if (*gran_type == CELL)
		{
		  /* obtain cell level locks */
		  if(((column1*row1) + column1) > ((column2*row2) + column2))
		  {
			  cell_lock(row2, column2);
			  cell_lock(row1, column1);
		  }
		  else
		  {
			  cell_lock(row1, column1);
		  	  if(((column1*row1) + column1) != ((column2*row2) + column2))
			  	cell_lock(row2, column2);
		  }
		
		}
		else if (*gran_type == GRID)
		{
		  /* obtain grid level lock*/
		  grid_lock();
		}


		temp = grid[row1][column1];

		
		#ifdef WINDOWS
		Sleep(1000);
        	#endif

	  	#ifdef UNIX
		sleep(1);
		#endif

		grid[row1][column1]=grid[row2][column2];
		grid[row2][column2]=temp;


		if (*gran_type == ROW)
		{
		  /* release row level locks */
		  row_unlock(row2);
		  row_unlock(row1);
		}
		else if (*gran_type == CELL)
		{
		  /* release cell level locks */
		  cell_unlock(row2, column2);
		  cell_unlock(row1, column1);

		}
		else if (*gran_type == GRID)
		{
		  /* release grid level lock */
		  grid_unlock();
		}


	}

	/* does this need protection? */
	#ifdef UNIX
	pthread_mutex_lock(&m_nthreads);
	#endif

	#ifdef WINDOWS
	WaitForSingleObject(m_nthreads, INFINITE);
	#endif

	threads_left--;

	#ifdef UNIX
	pthread_mutex_unlock(&m_nthreads);
	#endif

	#ifdef WINDOWS
	ReleaseMutex(m_nthreads);
	#endif

	if (threads_left == 0){  /* if this is last thread to finish*/
	  time(&end_t);         /* record the end time*/
	}
	#ifdef UNIX
	return NULL;
    #endif
    
    #ifdef WINDOWS
    return 0;
    #endif
}	







int main(int argc, char **argv)
{


	int nthreads = 0;
	
	#ifdef UNIX
	pthread_t threads[MAXTHREADS];
	#endif
	
	#ifdef WINDOWS
	HANDLE threads[MAXTHREADS];
	#endif
	
	grain_type rowGranularity = NONE;
	long initSum = 0, finalSum = 0;
	int i;

	
	if (argc > 3)
	{
		gridsize = atoi(argv[1]);					
		if (gridsize > MAXGRIDSIZE || gridsize < 1)
		{
			printf("Grid size must be between 1 and 10.\n");
			return(1);
		}
		nthreads = atoi(argv[2]);
		if (nthreads < 1 || nthreads > MAXTHREADS)
		{
			printf("Number of threads must be between 1 and 1000.");
			return(1);
		}

		if (argv[3][1] == 'r' || argv[3][1] == 'R')
			rowGranularity = ROW;
		if (argv[3][1] == 'c' || argv[3][1] == 'C')
			rowGranularity = CELL;
		if (argv[3][1] == 'g' || argv[3][1] == 'G')
		  rowGranularity = GRID;
			
	}
	else
	{
		printf("Format:  gridapp gridSize numThreads -cell\n");
		printf("         gridapp gridSize numThreads -row\n");
		printf("         gridapp gridSize numThreads -grid\n");
		printf("         gridapp gridSize numThreads -none\n");
		return(1);
	}

	printf("Initial Grid:\n\n");
	initSum =  InitGrid(grid, gridsize);
	PrintGrid(grid, gridsize);
	printf("\nInitial Sum:  %d\n", initSum);
	printf("Executing threads...\n");

	/* better to seed the random number generator outside
	   of do swaps or all threads will start with same
	   choice
	*/
	srand((unsigned int)time( NULL ) );
	
	time(&start_t);
	for (i = 0; i < nthreads; i++)
	{
        #ifdef UNIX
		if (pthread_create(&(threads[i]), NULL, do_swaps, (void *)(&rowGranularity)) != 0)
		{
			perror("thread creation failed:");
			exit(-1);
		} 
		#endif
		
		#ifdef WINDOWS
		arguments arg;
		arg.g_type = &rowGranularity;
		threads[i] = CreateThread(NULL, 0, do_swaps, &arg, 0, &arg.id);
		#endif
	}


	for (i = 0; i < nthreads; i++)
	{
        #ifdef UNIX
		pthread_detach(threads[i]);
		#endif
		
		#ifdef WINDOWS
		CloseHandle(threads[i]);
		#endif
    }


	while (1)
	{
        #ifdef UNIX
		sleep(2);
		#endif
		
		#ifdef WINDOWS
		Sleep(2000);
        #endif
        if (threads_left == 0)
		  {
		    fprintf(stdout, "\nFinal Grid:\n\n");
		    PrintGrid(grid, gridsize);
		    finalSum = SumGrid(grid, gridsize); 
		    fprintf(stdout, "\n\nFinal Sum:  %d\n", finalSum);
		    if (initSum != finalSum){
		      fprintf(stdout,"DATA INTEGRITY VIOLATION!!!!!\n");
		    } else {
		      fprintf(stdout,"DATA INTEGRITY MAINTAINED!!!!!\n");
		    }
		    fprintf(stdout, "Secs elapsed:  %g\n", difftime(end_t, start_t));

		    exit(0);
		  }
	}	
	
	
	return(0);
	
}






