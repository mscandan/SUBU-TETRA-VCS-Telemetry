if __name__ == "__main__":
    import requests
    import sys

    api = "http://yusufarabaci.com/data.php?"
    api += sys.argv[1]
    data = {
        'data' : sys.argv[1]
    }
    r = requests.post(url = api, data = data)
    print(r.text)
