#　関数のテンプレート

```
Function 関数名{
  param
  (
     引数名1
    ,引数名2
    ,引数名3
  )
  process
  {
    #処理
  }
}
```

引数の定義

|パラメータ|意味|値の詳細|
|---|---|---|
|Position|引数の位置||
|Mandatory|入力必須項目。指定されなかった場合、入力を求める。|$true:必須, $false:省略可|
|ParameterSetName|コマンドオプション名| ex: 指定文字列をParamにした場合、Powershell上で`Command -Param`と使用できる。<br /> 注意: ParameterSetNameを指定した場合、値の検証されない。 |

|検証|意味|詳細|
|---|---|---|
|ValidateNotNullOrEmpty()|Nullか、空の場合、エラーメッセージを出力する。|NG対象: 指定なし or ""|
|ValidateScript(Script)|値を検証するスクリプト|`$PSItem` - 指定されたアイテム<br /> `$PSItem | %{ 検証処理 }` - 複数の場合。1つでも$falseになったら受付を拒否できる。|

|型|意味|
|---|---|
|string|文字列|

Example1
Null, 空をしない。Nullの場合は、入力を求める。(空文字""指定の場合はエラーとなる。)
```
[Parameter(Position = 0, Mandatory = $true, ParameterSetName = "Path")]
[ValidateNotNullOrEmpty()]
[string]$Path,
```

Example2
検証：パスの生存確認
```
[Parameter(Position = 0, Mandatory = $true, ParameterSetName = "File")]
[ValidateScript({$PSItem | ForEach-Object {(Test-Path $_)})]
[string[]]$Files
```
検証：拡張子の限定
```
[ValidateScript({[System.IO.Path]::GetExtension($_) -ieq ".ps1"})]$File
```

Example3
