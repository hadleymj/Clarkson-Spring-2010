/************************
CS444/544 Operating System

HW8: Synchornization

Locking scheme description:
        
UNIX: Pthread mutex and condition variable
WINDOWS: Mutex and Event Objects

See corresponding README for complete description

        
Name: Mike Hadley and Jim Licata

Clarkson University  SP2010
 **************************/ 

/***** Includes *****/
#include <stdio.h>
#include <stdlib.h>

#ifdef UNIX
	#include <pthread.h>
	#include <unistd.h>
#endif

#ifdef WINDOWS
	#include <windows.h>
#endif


/***** Defines *****/
#define PRODUCER_SLEEP_S	2		//in seconds
#define CONSUMER_SLEEP_S	1
#define QUEUESIZE 			5
#define LOOP 				10


/***** Function prototypes *****/
void *producer (void *args);
void *consumer (void *args);

/***** Queue struct *****/
typedef struct {
	int buf[QUEUESIZE];
	long head, tail;
	int full, empty;  //you may need or may not
	/*Declare the locks here */

	//Declare locks for windows, along with event handles
	#ifdef WINDOWS
	 HANDLE lock, e_Con, e_Pro; // Add Handles for the mutex, and two events (one for producer, one for consumer)
	#endif

	#ifdef UNIX
		// One mutex, one condition variable
		pthread_mutex_t m;
		pthread_cond_t c; 
	#endif


	

} queue;

/***** Queue function prototypes  *****/
queue *queueInit (void);
void queueDelete (queue *q);
void queueAdd (queue *q, int in);
void queueDel (queue *q, int *out);

/***** main *****/
int main ()
{
	queue *fifo;
	
	int i;

    /******one producer and multiple consumer********/
	#ifdef UNIX    
	pthread_t pro, con[LOOP];
	#endif
	
	#ifdef WINDOWS
	HANDLE pro, con[LOOP];

	#endif
	
        

	fifo = queueInit ();
	if (fifo ==  NULL) {
		fprintf (stderr, "main: Queue Init failed.\n");
		exit (1);
	}
	
	#ifdef UNIX
		pthread_create (&pro, NULL, producer, fifo);
		for(i=0; i<LOOP; i++)
		{
			pthread_create (&con[i], NULL, consumer, fifo);
		}
	#endif
	
	#ifdef WINDOWS
		pro = CreateThread(NULL, 0,(LPTHREAD_START_ROUTINE) producer, fifo, 0, NULL);
		for(i=0; i<LOOP; i++)
		{
		    con[i] = CreateThread(NULL, 0,(LPTHREAD_START_ROUTINE) consumer, fifo, 0, NULL);		
		}
	#endif
	
	
	#ifdef UNIX
		pthread_join (pro, NULL);
		for(i=0; i<LOOP; i++)
		{
			pthread_join (con[i], NULL);	
		}
	#endif
	
	#ifdef WINDOWS
		WaitForSingleObject(pro, INFINITE);
        /*******Wait for all the threads complete**********/      
		WaitForMultipleObjects(LOOP, con, TRUE, INFINITE);	

	#endif
	
	queueDelete (fifo);

    #ifdef WINDOWS
	system("pause");
	#endif
	
	return 0;
}

/***** producer *****/
void *producer (void *q)
{

	queue *fifo;
	int i;

	fifo = (queue *)q;


    /**
      obtain the lock and release the lock somewhere
     **/

	for (i = 0; i < LOOP; i++) 
	{

        /*******Obtain the locks somewhere here **********/

	#ifdef UNIX
	pthread_mutex_lock(&fifo->m); // Grab the lock
	while (fifo->full) 
	{
		//If the buffer is full, lets wait until a consumer wakes us up (we use mesa semantics here)
		pthread_cond_wait(&fifo->c,&fifo->m);
	}
	#endif
		
        #ifdef WINDOWS

	// Grab the mutex
        WaitForSingleObject(fifo->lock, INFINITE);
	if(fifo->full)
        {
	   // If the buffer is full, lets release mutex (to allow the consumer to grab mutex and delete an item from the queue)
	      ReleaseMutex(fifo->lock);
	   // Wait for the e_Pro event, signaled by the consumer upon item deletion
           WaitForSingleObject(fifo->e_Pro, INFINITE);
	   // Grab the mutex again and proceed once the buffer isn't full anymore
           WaitForSingleObject(fifo->lock, INFINITE);
        }
        
        #endif
			
			
		queueAdd (fifo, i+1);
		printf ("producer: produced %d th.\n",i+1);
		
		/* sleep */
		#ifdef UNIX
			usleep ( PRODUCER_SLEEP_S * 1000000); 
		#endif
		
		#ifdef WINDOWS
			Sleep ( PRODUCER_SLEEP_S * 1000);	
		#endif

        /*******Release the locks**********/
        #ifdef UNIX
	pthread_mutex_unlock(&fifo->m);
	/* signal after you unlock */
	pthread_cond_signal(&fifo->c);
        #endif

        
        #ifdef WINDOWS
        //Release the mutex, set the consumer event if a consumer thread is waiting because the buffer is empty
        ReleaseMutex(fifo->lock);
        SetEvent(fifo->e_Con); // wake up exactly one consumer thread and reset the event
        #endif
	}

	return (NULL);
}

/***** consumer *****/
void *consumer (void *q)
{
	queue *fifo;
	int d;

	fifo = (queue *)q;

    /**
      obtain the lock and release the lock somewhere
     **/

    #ifdef UNIX
    pthread_mutex_lock(&fifo->m); // Grab the lock
	while (fifo->empty) 
	{
		//If the buffer is empty, lets wait until a producer wakes us up (we use mesa semantics here)
    		pthread_cond_wait(&fifo->c,&fifo->m);
	}
	#endif
	
	
    #ifdef WINDOWS

  
    WaitForSingleObject(fifo->lock, INFINITE);
    if(fifo->empty)
    {
	   // If the buffer is full, lets release mutex (to allow the consumer to grab mutex and delete an item from the queue)
       ReleaseMutex(fifo->lock);
	   // Wait for the e_Pro event, signaled by the consumer upon item deletion
       WaitForSingleObject(fifo->e_Con, INFINITE);
	   // Grab the mutex again and proceed once the buffer isn't full anymore
       WaitForSingleObject(fifo->lock, INFINITE);
    }
   //Wait on the consumer mutex

    #endif
	
	/* sleep */
	#ifdef UNIX
		usleep ( CONSUMER_SLEEP_S * 1000000); 
	#endif

	#ifdef WINDOWS
		Sleep ( CONSUMER_SLEEP_S * 1000);	
	#endif

    
	queueDel (fifo, &d);
	printf ("------------------------------------>consumer: recieved %d.\n", d);		

    #ifdef UNIX
	pthread_mutex_unlock(&fifo->m);
	/* signal after you unlock */
	pthread_cond_signal(&fifo->c);
    #endif


    #ifdef WINDOWS
    //Release the Mutex, set the Producer Event
    ReleaseMutex(fifo->lock);
    SetEvent(fifo->e_Pro);
    #endif
	

	return (NULL);
}


/***** queueInit *****/
queue *queueInit (void)
{
	queue *q;

	q = (queue *)malloc (sizeof (queue));
	if (q == NULL) return (NULL);

	q->empty = 1;
	q->full = 0;
	q->head = 0;
	q->tail = 0;

    /*Initialize the locks here	*/
    #ifdef WINDOWS
	// Create Mutex object, as well as two event objects
	q->lock = CreateMutex(NULL, FALSE, NULL);
	q->e_Con = CreateEvent(NULL, FALSE, FALSE, NULL);
	q->e_Pro = CreateEvent(NULL, FALSE, FALSE, NULL);

    #endif

    #ifdef UNIX
	//Initialize the condition variable
	pthread_cond_init(&q->c, NULL);
	#endif

	return (q);
}


/***** queueDelete *****/
void queueDelete (queue *q)
{

	/* free the locks here*/
	#ifdef WINDOWS
	//Close the handles to release the locks
	CloseHandle(q->lock);
	CloseHandle(q->e_Con);
	CloseHandle(q->e_Pro);
    #endif
    
    #ifdef UNIX
	pthread_cond_destroy(&q->c);
    #endif
	
	/* free memory used for queue */
	free (q);
}

/***** queueAdd *****/
void queueAdd (queue *q, int in)
{
	q->buf[q->tail] = in;
	q->tail++;
	if (q->tail == QUEUESIZE)
		q->tail = 0;
	if (q->tail == q->head)
		q->full = 1;
	q->empty = 0;

	return;
}

/***** queueDel *****/
void queueDel (queue *q, int *out)
{
	*out = q->buf[q->head];
	q->buf[q->head]=0;

	q->head++;

	if (q->head == QUEUESIZE)
		q->head = 0;
	if (q->head == q->tail)
		q->empty = 1;
	q->full = 0;

	return;
}
