def safe_calculator():
    print("=== 簡易電卓アプリ（安全版） ===")
    print("1: 足し算 (+)")
    print("2: 引き算 (-)")
    print("3: 掛け算 (*)")
    print("4: 割り算 (÷)")
    print("5: 終了\n")

    while True:
        try:
            op = int(input("操作を選んでください: "))
        except ValueError:
            print("数字を入力してください。\n")
            continue

        if op == 5:
            print("終了します。")
            break

        try:
            a = float(input("1つ目の数: "))
            b = float(input("2つ目の数: "))

            if op == 1:
                print(f"結果: {a + b}\n")
            elif op == 2:
                print(f"結果: {a - b}\n")
            elif op == 3:
                print(f"結果: {a * b}\n")
            elif op == 4:
                if b == 0:
                    print("0で割ることはできません。\n")
                else:
                    print(f"結果: {a / b}\n")
            else:
                print("1〜5の番号を選んでください。\n")

        except ValueError:
            print("数値を正しく入力してください。\n")

safe_calculator()