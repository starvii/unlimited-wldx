import argparse
import json
import uvicorn
import win32gui
from fastapi import FastAPI


class ArgData:
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


args = ArgData()


class Database:
    def __init__(self) -> None:
        super().__init__()
        # load json

    def load(self, filename):
        print(win32gui.GetOpenFileNameW(Title="select the examination database", DefExt="*.json"))


db = Database()


class Web:
    app = FastAPI()

    @staticmethod
    @app.post("/search")
    async def search():
        return ""

    @staticmethod
    @app.get("")
    async def help():
        return ""


def main():
    args.parse()
    db.load(args.filename)
    uvicorn.run(Web.app, host="127.0.0.1", port=args.port)


if __name__ == "__main__":
    main()
