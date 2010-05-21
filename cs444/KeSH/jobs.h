#define MAX_JOB_NUM 15
#include "parse.h"
#include <signal.h>

/* jobList struct
 * Defines a process, the command that forked it,
 * its PID, and its status 
 */

/* Status Flag is defined as follows:
 * 0 - Done
 * 1 - Stopped
 * 2 - Running
*/

typedef struct {
  char *command;
  int PID;
}jobList;

int isBackgroundJob(parseInfo *);
int addJob(char *, int, jobList * jobs []);
int killJob(int jobID, jobList * jobs []);
void printJobs(jobList * jobs []);
