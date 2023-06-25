import argparse
import json
import sys
from typing import List

import uvicorn
import win32gui
import win32con
from pydantic import BaseModel
from fastapi import FastAPI
from fastapi.responses import HTMLResponse


class Arguments:
    def __init__(self) -> None:
        self._port = 1995
        self._filename = "./exam.json"

    @property
    def port(self):
        return self._port

    @property
    def filename(self):
        return self._filename

    def parse(self):
        parser = argparse.ArgumentParser(
            prog="unlimited-wldx-backend(win32)",
            description="unlimited-wldx-backend",
            epilog="shaoziei zyson_wong"
        )
        parser.add_argument("-f", "--filename", type=str, help="examination database file. default: exam.json")
        parser.add_argument("-p", "--port", type=int, help="server port. default: 1995")
        _args = parser.parse_args()
        self._filename = _args.filename if _args.filename else "exam.json"
        self._port = _args.port if _args.port and (0 < _args.port < 65536) else 1995


args = Arguments()


class Entities:
    class RequestQuestion(BaseModel):
        q: str = ""
        a: List[str] = []

    class ResponseAnswer(BaseModel):
        q: str = ""
        a: List[str] = []
        ans: str = ""
        inf: str = ""


class Database:
    def __init__(self) -> None:
        super().__init__()
        # load json

    def load(self, filename):
        while 1:
            try:
                raise OSError("xxx")
            except Exception as e:
                err = f"无法读取文件[filename]：{e}"
                sys.stdout.write(err + "\n")
                win32gui.MessageBox(None, err, "错误", win32con.MB_OK | win32con.MB_ICONERROR)
                print(win32gui.GetOpenFileNameW(Title="select the examination database", DefExt="*.json"))


database = Database()


class Service:
    pass


service = Service()


class Web:
    app = FastAPI()

    @staticmethod
    @app.post("/search")
    async def search(q: Entities.RequestQuestion):
        return ""

    @staticmethod
    @app.get("")
    async def help():
        return HTMLResponse("""""")


def main():
    args.parse()
    database.load(args.filename)
    uvicorn.run(Web.app, host="127.0.0.1", port=args.port)


if __name__ == "__main__":
    main()
