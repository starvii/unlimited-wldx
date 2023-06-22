#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
availabe in python3
"""

from __future__ import print_function
import sys
import re
from xml.etree.ElementTree import ElementTree, Element, SubElement
if sys.version_info.major < 3:
    range = xrange
    input = raw_input

DOC_FILE = 'D:\\src\\unlimited-wldx\\sample\\sample.txt'
XML_FILE = 'D:\\src\\unlimited-wldx\\sample\\sample.xml'

def write_xml(questions):
    root = Element('questions')
    for q in questions:
        trunk = q[0]
        node = SubElement(root, 'question')
        node.text = trunk
        for l in q[1:]:
            opt = SubElement(node, 'option', {'correct': str(l[1])})
            opt.text = l[0]
    tree = ElementTree(root)
    tree.write(XML_FILE, encoding='utf-8')


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
            ret.append((line[n:], 1))
        else:
            ret.append((line[n:], 0))
    # print(ret)
    return ret

def main():
    lines = open(DOC_FILE, 'r').readlines()
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
    write_xml(questions)



if __name__ == '__main__':
    main()
