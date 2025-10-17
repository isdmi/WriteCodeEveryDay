import random

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
    return list(map(lambda x: x*2, lst))

def practice4(user_input):
    lst = user_input.split(" ")
    my_dict = {}
    for item in lst:
        my_dict[item] = my_dict.get(item, 0) + 1
    return my_dict

def practice5():
    numbers = [1, 4, 7, 8, 10, 13, 16]
    even_numbers = [num for num in numbers if num % 2 == 0]

    return even_numbers

def practice6(text):
    return text[::-1]

def practice7(user_input):
    my_dict = {1:"月曜日",2:"火曜日",3:"水曜日",4:"木曜日", 5:"金曜日", 6:"土曜日", 7:"日曜日"}
    print(my_dict.get(user_input, "不正な入力です"))

def practice8():
    ans = random.randint(1,10)
    user_input = 0
    while ans != user_input:
        user_input = (int(input("数字: ")))
    print("正解！")

def practice9():
    scores = {"国語": 75, "数学": 88, "英語": 92, "理科": 60, "社会": 70}
    average = sum(scores.values()) / len(scores)
    if (average >= 80):
        print("優秀")
    elif (average >= 60):
            print("普通")
    else:
        print("要改善")

practice9()