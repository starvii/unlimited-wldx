#!/usr/bin/env python
# -*- coding: utf-8 -*-

from __future__ import print_function
import os
import sys

if sys.version_info.major < 3:
    range = xrange
    input = raw_input

EXT = {'cs', 'resx', 'csproj'}
ROOT = 'D:/src/unlimited-wldx'

def replace(path):
    assert os.path.isfile(path)
    # assert os.path.splitext(path)[1][1:] in EXT
    try:
        c = open(path, 'rb').read()
        if b'\r\n' in c:
            c = c.replace(b'\r\n', b'\n')
            open(path, 'wb').write(c)
            sys.stderr.write(path + '\n')
    except Exception as e:
        sys.stderr.write(e)
        sys.stderr.write('\n')
    

def main():
    for root, _, files in os.walk(ROOT):
        for f in files:
            path = os.path.join(root, f)
            ext = os.path.splitext(path)[1][1:].lower()
            if ext in EXT:
                replace(path)


if __name__ == '__main__':
    main()
