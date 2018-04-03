import numpy as m
import zipfile
import tarfile
import os
def actions(path, name):
	with open(path+name+'.txt', 'r') as inf:
	    strings = inf.read().lower()
	    size = inf.tell()*8
	lettersCount = {}
	count = 0
	for i in range(len(strings)):
	    if 1072 <= ord(strings[i]) <= 1111 or 48<=ord(strings[i])<=62 or 97<=ord(strings[i])<=126:
	        if strings[i] in lettersCount.keys():
	            lettersCount[strings[i]] += 1
	        else:
	            lettersCount[strings[i]] = 1
	        count += 1
	H = 0
	for key in lettersCount.keys():
	    p = lettersCount[key]/count
	    print("letter: {} | count: {} | probability: {}%".format(key, lettersCount[key], p*100))
	    H += p*m.log2([1/p])[0]
	CoI = H*count
	print("Entrophy: {}".format(H))
	print("Count of Info: {}".format(CoI))
	print("size: {} bits".format(size))
	print("CoI/size = {}".format(CoI/size))
	with zipfile.ZipFile(path+name+'.bzip2.zip', 'w', zipfile.ZIP_BZIP2) as myzip:
	    myzip.write(path+name+'.txt')
	with zipfile.ZipFile(path+name+'.zip', 'w') as myzip:
	    myzip.write(path+name+'.txt')
	os.system('rar a "'+ path+name+'.rar" "'+path+name+'.txt"')
	with tarfile.open(path+name+".tar.gz", "w:gz") as tar:
	    tar.add(path+name+'.txt')
	with tarfile.open(path+name+".tar.xz", "w:xz") as tar:
	    tar.add(path+name+'.txt')
	print('CoI/size(bzip2): {}'.format(CoI/os.path.getsize(path+name+'.bzip2.zip')*8))
	print('CoI/size(zip): {}'.format(CoI/os.path.getsize(path+name+'.zip')*8))
	print('CoI/size(rar): {}'.format(CoI/os.path.getsize(path+name+'.rar')*8))
	print('CoI/size(gz): {}'.format(CoI/os.path.getsize(path+name+".tar.gz")*8))
	print('CoI/size(xz): {}'.format(CoI/os.path.getsize(path+name+".tar.xz")*8))

	

actions('E:\\Study (H)\\', 'poderev')
actions('E:\\Study (H)\\', 'trinadcyatui')
actions('E:\\Study (H)\\', 'PCI')
actions('E:\\Study (H)\\', 'result')