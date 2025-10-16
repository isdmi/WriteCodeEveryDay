def practice1():
    numbers = [3, 10, 1, 8, 5, 9]
    print(f"最大値は {max(numbers)} 最小値は {min(numbers)}")

def practice2():
    total = 0
    i = 1
    while total <= 100:
        total = total + i
        i = i + 1
    print(f"合計は {total}")

def double_list(lst):
    lst = list(map(func, lst))
    return lst

def func(x):
    return x*2

def practice4(user_input):
    lst = user_input.split(" ")
    my_dict = {}
    for item in lst:
        if item in my_dict:
            my_dict[item] = my_dict[item] + 1
        else:
            my_dict[item] = 1
    return my_dict

def practice5():
    numbers = [1, 4, 7, 8, 10, 13, 16]
    even_numbers = [num for num in numbers if num % 2 == 0]

    return even_numbers

def practice6(str):
    return str[::-1]

def practice7(user_input):
    my_dict = {1:"月曜日",2:"火曜日",3:"水曜日",4:"木曜日", 5:"金曜日", 6:"土曜日", 7:"日曜日"}
    if user_input in my_dict:
        print(my_dict[user_input])
    else:
        print("不正な入力です")

practice7(int(input("数字: ")))