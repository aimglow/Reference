# PowerShell Function

### 関数定義ルール
* Process句は省略できますが、Paramが長文になった際の区別として明示します。
* Paramは関数名(Param1, Param2)とも記述できますが、パラメータを詳細に指定した方が使い勝手がよくなる為、Param句を記述します。
* CmdLetBindingを指定する。

```
#!      f.template.ps1 0.0.1

Function FunctionName(){
    <#
    .SYNOPSIS
    関数の簡単な説明

    .DESCRIPTION
    関数の詳細な説明

    .PARAMETER パラメータ名1
    パラメータ名1の説明

    .PARAMETER パラメータ名2
    パラメータ名2の説明
    
    .PARAMETER パラメータ名3
    パラメータ名3の説明
    
    .INPUTS
    パイプラインで渡される値の説明（パイプラインサポート対象外: None, does not support the pipline）
    
    .OUTPUTS
    出力内容

    .EXAMPLE
    コマンド使用例1

    .EXAMPLE
    コマンド使用例2

    .EXAMPLE
    コマンド使用例3
    #>

    [CmdletBinding(
        ConfirmImpact           = "High" # "Low","Middle","High" [default: "Medium"] : 確認要求レベル
    #  ,HelpURI                 = ""     # <string> : オンラインヘルプURI
    #  ,SupportsPaging          = $false # <bool> [default: $false] $true : 大規模データベースの取得などに必要な機能をバインドする。First(最初のn行), Skip(n行スキップ), IncldeTotalCount(データセット内のオブジェクト数)
       ,SupportsShouldProcess   = $true  # <bool> [default: $false] $true : コマンド実行確認プロセスに必要な機能をバインドする。WhatIf(影響範囲の表示), Confirm(コマンド実行確認)
    #  ,PositionalBinding       = $false # <bool> [default: $false] $true : Positionパラメータを明示する必要がある。パイプラインなどで複数渡す場合に（Position=0を複数設定するなど）使用する。, $false : Positionパラメータは定義されている変数を、上から順に自動附番する。
    #  ,DefaultParameterSetName = ''     # <string> : 参照: Param句のParameter(ParameterSetName)
    )]
    Param(
        [Parameter(
            Mandatory   =      $true   # <bool> [default: $false] $true : 引数がNullの場合に入力を要求する。
        #  ,Position    =      0       # CmdletBindingでPositionalBindingを指定した場合に使用する。
        #  ,HelpMessage =      ""      # <string> : Mandatoryで要求した際、「!?」でパラメータに対するメッセージを表示できる。
        #  ,ParameterSetName = ""      # <string> : 複数のswitch型パラメータでswitch分岐する場合に使用する。指定する場合は、CmdletBindingでDefaultを指定する必要がある。（使用例: https://github.com/Dejulia489/AzurePipelinesPS/blob/6ff1b3061e9e818ef4cea733b07a6dc154dbe891/AzurePipelinesPS/Public/Write-APLogMessage.ps1)
        )]
        [ValidateNotNullOrEmpty()]
    #   [ValidateScript({ Test-Example $_ -eq 0 })]
    #   [ValidateSet("PermissionText1","PermissionText2")]
        [string]$Path
    )
    Begin{}  # 1回のプロセスが動く前に実行する。(ForEach-Object内で実行された場合は、毎度実行される。)
    Process{  # 値が渡される毎に実行される。
        Write-Verbose "詳細説明。各フローの遷移 : -Verbose指定時に出力"
        Write-Debug   "開発者用のコメント       : -Debug指定時に出力"
        Write-Host    "ホストに出力される。パイプラインに渡されない"
        Write-Output  "パイプラインに渡す。パイプラインに渡さない場合は、ホストに出力される。"
    }
    End{}     # 最後のプロセスが実行された後に実行される。
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

