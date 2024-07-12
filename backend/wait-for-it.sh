#!/usr/bin/env bash
#   Use this script to test if a given TCP host/port are available

TIMEOUT=15
QUIET=0
EXIT_CODE=1

print_usage() {
  echo "Usage: $0 host:port [-t timeout] [-- command args]"
  echo "  -q | --quiet                      Do not output any status messages"
  echo "  -t TIMEOUT | --timeout=timeout    Timeout in seconds, zero for no timeout"
  echo "  -- COMMAND ARGS                   Execute command with args after the test finishes"
  exit 1
}

wait_for() {
  for i in `seq $TIMEOUT` ; do
    nc -z $HOST $PORT > /dev/null 2>&1
    result=$?
    if [ $result -eq 0 ] ; then
      if [ $QUIET -eq 0 ] ; then
        echo "$HOST:$PORT is available after $i seconds"
      fi
      EXIT_CODE=0
      break
    fi
    sleep 1
  done
}

while [ $# -gt 0 ]
do
  case "$1" in
    *:* )
    HOST=$(echo $1 | cut -d : -f 1)
    PORT=$(echo $1 | cut -d : -f 2)
    shift 1
    ;;
    -q | --quiet)
    QUIET=1
    shift 1
    ;;
    -t)
    TIMEOUT=$2
    if [ $TIMEOUT -lt 1 ] ; then
      echo "Invalid timeout: $TIMEOUT"
      exit 1
    fi
    shift 2
    ;;
    --timeout=*)
    TIMEOUT=$(echo $1 | cut -d = -f 2)
    if [ $TIMEOUT -lt 1 ] ; then
      echo "Invalid timeout: $TIMEOUT"
      exit 1
    fi
    shift 1
    ;;
    --)
    shift
    CMD="$@"
    break
    ;;
    -*)
    echo "Unknown option: $1"
    print_usage
    ;;
    *)
    echo "Unknown argument: $1"
    print_usage
    ;;
  esac
done

if [ "$HOST" = "" -o "$PORT" = "" ] ; then
  echo "Error: you need to provide a host and port to test."
  print_usage
fi

wait_for

if [ "$CMD" != "" ] ; then
  exec $CMD
else
  exit $EXIT_CODE
fi

