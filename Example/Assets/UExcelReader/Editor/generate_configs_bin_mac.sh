#!/bin/sh

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

case $OSTYPE in
  darwin*)
    OS=macosx
    ;;
  linux*)
    OS=linux
    ;;
  *)
    echo "Unsupported OS: $OSTYPE" > /dev/stderr
    exit 1
    ;;
esac

uname_m="$(uname -m)"
case "$uname_m" in
  x86_64)
    ARCH="x64"
    ;;
  i686)
    ARCH="x86"
    ;;
  *)
    echo "Unsupported Arch: $uname_m" > /dev/stderr
    exit 1
    ;;
esac

TABTOY="$DIR/tabtoy/${OS}_${ARCH}/tabtoy"
chmod +x "$TABTOY"

exportJson=true
exportLua=true

if [ $4 = "null" ]; then 
  exportJson=false
fi

if [ $5 = "null" ]; then
  exportLua=false
fi 

if [[ $exportJson = true && $exportLua = true ]]; then
  "$TABTOY" -mode=exportorv2 -combinename=$1 -csharp_out="$2" -binary_out="$3" -json_out="$4" -lua_out="$5" "${@:6}"
fi

if [[ $exportJson = true && $exportLua = false ]]; then
  "$TABTOY" -mode=exportorv2 -combinename=$1 -csharp_out="$2" -binary_out="$3" -json_out="$4" "${@:6}"
fi

if [[ $exportJson = false && $exportLua = true ]]; then
  "$TABTOY" -mode=exportorv2 -combinename=$1 -csharp_out="$2" -binary_out="$3" -lua_out="$5" "${@:6}"
fi

if [[ $exportJson = false && $exportLua = false ]]; then
  "$TABTOY" -mode=exportorv2 -combinename=$1 -csharp_out="$2" -binary_out="$3" "${@:6}"
fi

