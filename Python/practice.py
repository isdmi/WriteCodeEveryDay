import numpy as np

def practice1():
    print("Hello World")

def practice2():
    user_input1 = int(input("数字1: "))
    user_input2 = int(input("数字2: "))
    print(f"合計は {user_input1 + user_input2} です")

def practice3():
    user_input1 = int(input("数字: "))
    if user_input1 % 2 == 0:
        print("偶数です")
    else:
        print("奇数です")

def practice4():
    for i in range(1, 21):
        if i % 3 == 0:
            print("Fizz")
        else:
            print(f"{i}")
def practice5():
    user_input1 = int(input("数字: "))
    total = 0
    for i in range(1, user_input1 + 1):
        total = total + i
    print(f"{total}")

def practice6():
    numbers = [3, 7, 2, 8, 5]
    print(max(numbers))

def practice7(x:int, y:int):
    return x * y

def practice8():
    numbers = [10, 20, 30, 40, 50]
    print(np.mean(numbers))

def practice9():
    user_input1 = str(input("文字列："))
    print(len(user_input1))

def practice10():
    for i in range(1, 10):
        for j in range(1, 10):
            print(f"{i*j:2}", end=" ")
        print()  # 行の終わりで改行
practice10()