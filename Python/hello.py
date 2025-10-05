#関数定義
def func(num1, num2 ,num3 ):
    print(num1, num2, num3 )

#if文
def first_item(items):
        if (len(items) > 0):
            return items[0]
        else:
             return None

#for文
def loop_func():
    items = [1,2,3]
    for i in items:
        print(f'変数iの値は{i}')

    chars = 'word'
    for count, char in enumerate(chars):
        print(f'{count}番目の値は{char}')

    nums = [2,4,6,8]
    for n in nums:
        if n % 2 == 1: 
            break
    else:
        print('奇数の値を含めてください')

x,y,z = 4,5,6
func(x,y,z)
print(first_item(['book',['contents']]))
loop_func()