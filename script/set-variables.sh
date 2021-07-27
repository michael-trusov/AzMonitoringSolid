#!/bin/bash

# This script is used to to set environments variables
#
# Usage:
#  $ ./set-variables.sh param1
# * param1: path for json

if [ $# -ne 1 ] ;
then
    echo "1 Arguments needed: set-variables.sh "
else

    values=`cat $1`
    echo $values

    for s in $(echo $values | jq -r "to_entries|map(\"\(.key)=\(.value|tostring)\")|.[]" ); do
    echo $s >> $GITHUB_ENV
    done

fi
