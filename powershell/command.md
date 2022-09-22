# PowerShell Command
調べて約に立ったCommandをここに整理する。

# Index
1. 機密文字列の暗号化と、復号 -> パスワードの保存
2. 規定のフォルダを取得(ex. MyDocumentフォルダ、ユーザーフォルダなど。

# 1. 機密文字列の暗号化と、復号

暗号化例：
```powershell
$uuid = [GUID]::NewGuid()
$secstr = ConvertTo-SecureString $passwd -AsPlainText -Force                # 文字列$passwdを暗号化
$secstr | ConvertFrom-SecureString | Out-File $uuid.Guid                    # 作成した暗号化文字列を、GUIDをフォルダ名として、ファイルに出力
$label + "," + $tmpgid.Guid | Out-File -Append -FilePath secstrlist.txt    # secstrlist.txtに暗号化文字列として出力($labelは、暗号化した文字列のラベル)
```
上記のように暗号化したものを復号：
```powershell
$tmptar = Get-Content secstrlist.txt | Select-String $target
if( $tmptar.Line.Split(",")[0] -ne $target ){ return } # Select-Stringは部分一致で取得してしまうので、完全一致したものだけを取得
# TODO: Select-Stringで取得した文字列は１行だけとは限らない。したがって、取得結果をループ処理し、完全一致するものを取得したい。

# TODO: 取得した$tmptarのファイルが存在することを確認。

$impsec = Get-Content $tmptar.Line.Split(",")[1] | ConvertTo-SecureString
$bstrig = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($impsec)
[System.Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstrig) | clip

```


# 2. 規定のフォルダを取得(ex. MyDocumentフォルダ、ユーザーフォルダなど。
```powershell
[System.Environment]::GetFolderPath("UserProfile")  # ユーザーフォルダ
```
SpecialFolderで指定できる文字列のリストを取得
```powershell
[System.Environment]::xxx
```
