#node generator
output=open('result.txt','w')
n=int(input('Insert number of node: '))
output.write(str(n)+'\n')
for i in range(1,n):
    output.write(str(i)+' '+str(i+1)+'\n')