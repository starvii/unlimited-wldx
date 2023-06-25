#!/usr/bin/python
# -*- coding: utf-8 -*-

from typing import List, Tuple
import json

def string_find_all(subtext: str, text: str) -> Tuple[int]:
    indexes = []
    idx = -1
    while idx < len(text):
        idx = text.find(subtext, idx + 1)
        if idx < 0:
            break
        indexes.append(idx)
    return tuple(indexes)

def replace_line_feed_in_segment(text: str, quotes_index: List[int]) -> str:
    plain_index: List[int] = []
    format = quotes_index[0] == 0
    plain_index.append(0)
    plain_index.extend(quotes_index)
    plain_index.append(len(text))
    buffer = []
    for idx in range(len(plain_index) - 1):
        idx0 = plain_index[idx]
        idx1 = plain_index[idx + 1]
        if format:
            buffer.append(text[idx0 + 1 : idx1].replace("\n", "\\n"))
        else:
            buffer.append(text[idx0 + 1 : idx1])
        format = not format
    return "".join(buffer)

def text_to_json(formatted_text: str) -> str:
    buffer = []
    lines = formatted_text.split("\n")
    for line in lines:
        a = line.split("\t")
        assert len(a) == 3
        j = {}
        j["q"] = a[0].strip()
        j["a"] = []
        j["ans"] = a[2].strip()
        if len(a[1].strip()) == 0 or a[1].strip() == "\\":
            pass
        elif "|" in a[1]:
            aa = [x.strip() for x in a[1].strip().split("|")]
            j["a"].extend(aa)
        buffer.append(json.dumps(j, ensure_ascii=False, separators=(",", ":")))
    return "\n".join(buffer)

with open("exam.txt", "rb") as f:
    lines = [x.strip().decode() for x in f.readlines() if len(x.strip()) > 0]
    print(len(lines))
    text = "\n".join(lines)
    quotes_index = sorted(list(set(string_find_all('"', text)) - set([x + 1 for x in string_find_all(r'\"', text)])))
    assert(len(quotes_index) % 2 == 0)
    print(quotes_index)
    formatted_text = replace_line_feed_in_segment(text, quotes_index)
    with open("exam1.txt", "w") as fw:
        fw.write(formatted_text)
    json_text = text_to_json(formatted_text)
    with open("exam.json", "w") as fj:
        fj.write(json_text)

