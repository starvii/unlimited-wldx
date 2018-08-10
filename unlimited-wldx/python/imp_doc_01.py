#!/usr/bin/env python
# -*- coding: utf-8 -*-

from __future__ import print_function
import sys
import re
import sqlite3
if sys.version_info.major < 3:
    range = xrange
    input = raw_input

DOCFILE = 'D:\\src\\unlimited-wldx\\sample\\sample.txt'
DBFILE = 'D:\\src\\unlimited-wldx\\sample\\sample.sqlite'

def write_db(questions):
    sql_trunk = '''
INSERT INTO `trunk` (`trunk`, `type`) VALUES(?, ?);
'''
    sql_opt = '''
INSERT INTO `options` (`tid`, `option`, `result`) VALUES(?, ?, ?);
'''

    with sqlite3.connect(DBFILE) as conn:
        c = conn.cursor()
        for q in questions:
            c.execute(sql_trunk, (q[0], 0))
            tid = c.lastrowid
            for l in q[1:]:
                c.execute(sql_opt, (tid, l[0], l[1]))
                

def extract(lines):
    # print(lines)
    # print('================')
    ret = list()
    l = lines[0]
    rn = re.findall(r'^\d+\.\s*', l)
    assert len(rn) > 0
    n = len(rn[0])
    rn = re.findall(r'[\(（][A-Z][\)）]', l)
    assert len(rn) > 0
    answer = rn[0][1:-1]
    # print(answer)
    ret.append(l[n:])
    for line in lines[1:]:
        rn = re.findall(r'^[A-Z]\.\s*', line)
        assert len(rn) > 0
        n = len(rn[0])
        if line[0] in answer:
            ret.append((line[n:], True))
        else:
            ret.append((line[n:], False))
    # print(ret)
    return ret

def main():
    lines = open(DOCFILE, 'r').readlines()
    state = 0
    questions = list()
    buf = None
    for line in lines:
        l = line.strip()
        if state == 0:
            rn = re.findall(r'^\d+\.\s*', l)
            if len(rn) > 0:
                state = 1
                buf = list()
                buf.append(l)
        elif state == 1:
            if len(l) == 0:
                state = 0
                q = extract(buf)
                questions.append(q)
                buf = None
            else:
                buf.append(l)
    if buf:
        q = extract(buf)
        questions.append(q)
    write_db(questions)



if __name__ == '__main__':
    main()
