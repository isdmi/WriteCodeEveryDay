import os
import random

#単語帳アプリ
def Tango():
    print("=== 単語帳アプリ ===")
    filename = "tango_list.txt"
    operation_text = "1. 登録\r\n2. クイズ開始\r\n3. 終了\r\n番号を選んでください:"
    word_text = "英単語を入力:"
    mean_text = "意味を入力:"

    words = {}
    if os.path.exists(filename):
        with open(filename, "r", encoding="utf-8") as f:
            for line in f:
                if ":" in line:
                    word, mean = line.strip().split(":", 1)
                    words[word] = mean

    total = 0
    correct = 0

    while True:     
        try:
            text = input(operation_text)
            operation = int(text)
        except ValueError:
            print("⚠️ 数字を入力してください。\n")
            continue            

        if (operation == 1):
            addword = input(word_text)
            addmean = input(mean_text)
            words[addword] = addmean
            print("→ 登録しました！")
            print()
        elif (operation == 2):
            if not words:
                print("📭 現在、単語帳はありません。\n")
            else:
                # ランダム出題
                word, mean = random.choice(list(words.items()))
                answer = input(f"問題: 「{word}」の意味は？ ")

                total += 1  # クイズ回数増やす

                if answer == mean:
                    correct += 1
                    print("✅ 正解！\n")
                else:
                    print(f"❌ 不正解… 正しくは 「{mean}」 です。\n")
        elif (operation == 3):
            # ファイルに保存
            with open(filename, "w", encoding="utf-8") as f:
                for item in words:
                    f.write(item + "\n")

            if total > 0:
                rate = (correct / total) * 100
                print(f"出題数: {total}")
                print(f"正解数: {correct}")
                print(f"正解率: {rate:.1f}%")
            else:
                print("クイズは実施されませんでした。")

            print("👋 終了します。")
            break
        else:
            print("⚠️ 1〜3の番号を入力してください。\n")

Tango()