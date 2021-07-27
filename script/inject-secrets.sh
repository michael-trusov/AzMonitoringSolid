#!/bin/bash

# This script is used to remove secrets from github from the config files
#
# Usage:
#  $ ./inject-secrets.sh param1 [param2]
# * param1: apiManagementToken
# * param2: blobStorageConnectionString



if [ $# -ne 5 ] ;
then
    echo "4 Arguments needed: inject-secrets.sh \${apiManagementToken} \${blobStorageConnectionString}"
else

    apimToken=$(printf '%s\n' "$1" | sed -e 's|&|\\&|g');
    storageConnectionString=$(printf '%s\n' "$2" | sed -e 's|&|\\&|g');

    configs=(
        ./src/config.design.json 
        ./src/config.publish.json 
        ./src/config.runtime.json 
        ./scripts/generate.sh
    )

    for i in "${configs[@]}"; do
        sed -i "s|\${apimToken}|$apimToken|g" $i
        sed -i "s|\${storageConnectionString}|$storageConnectionString|g" $i
        sed -i "s|\${managementApiUrl}|$3|g" $i
        sed -i "s|\${apim_instance_name}|$4|g" $i
        sed -i "s|\${backendUrl}|$5|g" $i
    done
fi