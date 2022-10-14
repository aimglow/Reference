# PowerShell Function

### 関数定義ルール
* Process句は省略できますが、Paramが長文になった際の区別として明示します。
* Paramは関数名(Param1, Param2)とも記述できますが、パラメータを詳細に指定した方が使い勝手がよくなる為、Param句を記述します。
* CmdLetBinding

```
Function 関数名{
  Param
  (
     引数名1
    ,引数名2
    ,引数名3
  )
  Begin{}
  Process
  {
    #処理
  }
  End{}
}
```

## 引数の定義

### 引数定義ルール
* 検証スクリプトを指定しない場合は、HelpMessageを記述します。
* ParameterSetNameパラメータは、検証(Validate)や入力の要求(Mandatory)が動作しない場合がある為指定しません。
* Positionパラメータは指定しません。


### Example Code1
* Null, 空を許容しない。
* Nullの場合は、入力を求める。(空文字""指定の場合はエラーとなる。)
* 初期値を設定する。
```
[Parameter(Position = 0, Mandatory)]
[ValidateNotNullOrEmpty()]
[string]$Path = "c:\work",
```

Example2
検証：パスの生存確認
```
[Parameter(Position = 0, Mandatory = $true)]
[ValidateScript({$PSItem | ForEach-Object {(Test-Path $_)})]
[string[]]$Files
```
検証：拡張子の限定
```
[ValidateScript({[System.IO.Path]::GetExtension($_) -ieq ".ps1"})]$File
```

Example3

```

```

|パラメータ|意味|値の詳細|
|---|---|---|
|Position|引数の位置||
|Mandatory|入力必須項目。指定されなかった場合、入力を求めます。|$true:必須であることを示します。$trueの変わりに`Mandatroy`だけでも良いです。 <br /> $false:省略可であることを示しめします。$falseの代わりに-Mandatroy-を記述しないのでも良いです。|
|HelpMessage|値のヘルプ| 値を指定しなかった場合に、Mandatoryで値を求めることができます。その際`!?`を入力すると、ヘルプメッセ―ジを入力できます。そのメッセージを指定します。 |
|(使用しない) ParameterSetName|コマンドオプション名| ex: 指定文字列をParamにした場合、Powershell上で`Command -Param`と使用できます。<br /> 注意: ParameterSetNameを指定した場合、値は検証されないです。 |

|検証|意味|詳細|
|---|---|---|
|ValidateNotNullOrEmpty()|Nullか、空の場合、エラーメッセージを出力する。|NG対象: 指定なし or 空文字「""」|
|ValidateSet("Value1", "Value2")|指定した値に一致するものに制限します|
|ValidateScript(Script)|値を検証するスクリプト|`$PSItem` - 指定されたアイテム <br /> `$PSItem \| ForEach-Object{ 検証 } ` - 複数の場合。1つでも$falseになったら受付を拒否できる。|

|型|意味|
|---|---|
|string|文字列|
|string[]|配列文字列|
|switch|ブール値 : `Command -SwitchParam`と指定すると、`$SwitchParam`に`$true`が入る。<br \> `Command`に`-SwitchParam`を指定しなければ`$SwitchParam`は`$false`になる。<br/>switch型とする場合は、Mandatoryは`$false`指定する。|

初期値を設定する場合は、`[function]$Param = "初期値"`とする。

