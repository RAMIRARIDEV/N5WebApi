#!/usr/bin/env bash

# wait-for-it.sh host port -- command args...
# Espera a que un host y puerto estén disponibles antes de ejecutar un comando.

host="$1"
port="$2"
shift 2
cmd="$@"

echo "Esperando a $host:$port..."

while ! nc -z "$host" "$port"; do
  sleep 1
done

echo "$host:$port disponible. Ejecutando comando..."
exec $cmd