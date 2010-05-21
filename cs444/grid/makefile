
CC = gcc
#CFLAGS = -O
CFLAGS = -g -Wall -lpthread -DUNIX

LD = gcc
LDFLAGS = 

RM = /bin/rm -f

PROG = gridapp

#############################################
all: $(PROG)

gridapp: gridapp.c
	$(CC) -o gridapp gridapp.c $(CFLAGS)

clean:
	$(RM) $(PROG) *.o core *~











