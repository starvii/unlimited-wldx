本地服务器，使用`pyinstaller`生成可执行文件。

题库仅接受json格式（与【小包搜题】一致）

小包搜题 题库格式（一行一条）：

```json lines
{"q":"<题目>", "a":["A. xxx", "B. yyy", "C. zzz", "D. www"], "ans": "A"}

```

提交的数据格式：

```json
{"q": "<题目>", "a": [""]}
```

返回的数据格式：

```json
{"q":"<题目>", "a":["A. xxx", "B. yyy", "C. zzz", "D. www"], "ans": "A", "info": "method: 100%"}
```