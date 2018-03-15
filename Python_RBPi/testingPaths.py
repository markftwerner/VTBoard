import os, base64

def request(): #takes all files in current folder and read their text to a variable to print out
    for filename in os.listdir('.'):
        if filename.endswith('.py'):
            with open(os.path.join('.', filename)) as f:
                content = f.read()
                #str = base64.b64encode(imageFile.read())
                print (content)


#need base 64 data transfer
#need to watch for how much string data sent. Perhaps set in arrays. then sequence out.
print (os.listdir('.'))
#request()
