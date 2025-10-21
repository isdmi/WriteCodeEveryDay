import os

#ToDoアプリ
def ToDo():
    print("=== ToDo アプリ ===")
    filename = "todo_list.txt"
    operation_text = "1. 追加\r\n2. 一覧\r\n3. 削除\r\n4. 終了\r\n番号を選んでください:"
    add_text = "追加するToDoを入力してください："
    del_text = "削除するToDoを入力してください："

    todos = []
    if os.path.exists(filename):
        with open(filename, "r", encoding="utf-8") as f:
            todos = [line.strip() for line in f if line.strip()]

    while True:     
        try:
            text = input(operation_text)
            operation = int(text)
        except ValueError:
            print("⚠️ 数字を入力してください。\n")
            continue            

        if (operation == 1):
            addTodo = input(add_text)
            todos.append(addTodo)
            print(f"→「{addTodo}」を追加しました！")
            print()
        elif (operation == 2):
            if not todos:
                print("📭 現在、ToDoはありません。\n")
            else:
                for idx, item in enumerate(todos):
                    print(f"[{idx}] {item}")
                print()
        elif (operation == 3):
            if not todos:
                print("削除できるToDoがありません。\n")
                continue

            deleteTodo = input(del_text)
        
            delete_idx = int(deleteTodo)

            try:
                delete_idx = int(deleteTodo)
                if 0 <= delete_idx < len(todos):
                    removed = todos.pop(delete_idx)
                    print(f"→「{removed}」を削除しました！\n")
                else:
                    print("⚠️ 存在しませんでした。\n")
            except ValueError:
                print("⚠️ 数字を入力してください。\n")

        elif (operation == 4):
            # ファイルに保存
            with open(filename, "w", encoding="utf-8") as f:
                for item in todos:
                    f.write(item + "\n")
            print("👋 終了します。")
            break
        else:
            print("⚠️ 1〜4の番号を入力してください。\n")
ToDo()