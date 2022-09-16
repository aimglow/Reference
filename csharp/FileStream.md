# FileStream

## FileStreamとは
FileStreamとは、Streamの一種で、Fileの読み書きを行う為オブジェクトである。

> ToDo
> FileStreanオブジェクトと、読み書きそれぞれのオブジェクト(StreamWriter, StreamReader)が分かれているのは意味があるのか。
> 重要なポイントは、FileStreamオブジェクトで事足りるわけだから、わざわざ分ける方法を選ぶいみはないと感じている。
> 何か、分けることに意義があるなら今後のコーディングの糧にしたい。

## 同期と非同期
同期


## 参考
```csharp
static async void BinaryReadWriteAsync(byte[] data)
{
    // 読み書きするファイル（実行ファイルと同じフォルダに作られる）
    const string FilePath = @".\sample.dat";

    // バイナリファイル書き込み

    // ファイルを上書きモードで開く（ファイルがないときは作る）
    // 追加モードにするにはFileModeをAppendに変える
    using (var fs = new System.IO.FileStream(FilePath,System.IO.FileMode.Create, System.IO.FileAccess.Write))
    {
        // バイナリデータを非同期的に書き込む
        await fs.WriteAsync(data, 0, data.Length);
    } // usingを抜けるとき、ファイルへ完全に書き込まれる

    // バイナリファイル読み込み
    byte[] result; // データを格納する配列

    // ファイルを読み取りモードで開く
    using (var fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
    {
        // データ格納用の配列を確保する
        result = new byte[fs.Length];

        // バイナリデータを非同期的に読み込む
        await fs.ReadAsync(result, 0, (int)fs.Length);
    }

    // 読み込んだ内容をコンソールへ出力する
    for(int i=0; i<result.Length; i++)
    {
        Write($"{result[i]:X2} ");
        if (i % 16 == 7)
        Write(" ");
        if (i % 16 == 15)
        WriteLine();
    }
    WriteLine();
}
```
