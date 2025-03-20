#!/bin/sh
echo -ne '\033c\033]0;Linux Defender\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/LinuxDefender.x86_64" "$@"
