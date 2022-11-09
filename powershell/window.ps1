#操作したいウィンドウのタイトル
$MAIN_WINDOW_TITLE='chrome'
#ウィンドウの新しい幅 (ピクセル単位)
$width=1000
#ウィンドウの新しい高さ (ピクセル単位)。
$heigh=1000
#コントロールの左側の絶対画面座標。
$x=0
#コントロールの上部の絶対画面座標。
$y=0
Add-Type -AssemblyName UIAutomationClient
#検索をデスクトップから開始するためデスクトップを取得する
$root= [System.Windows.Automation.AutomationElement]::RootElement
#検索対象は子要素だけとする
$scope=[System.Windows.Automation.TreeScope]::Children
#とりあえずGet-Processで取得できた一つ目のハンドルを対象とする。
$hwnd=(Get-Process |?{$_.MainWindowTitle -match $MAIN_WINDOW_TITLE})[0].MainWindowHandle
#ハンドルからウィンドウを取得する
$window=[System.Windows.Automation.AutomationElement]::FromHandle($hwnd)
#ウィンドウサイズの状態を把握するためにWindowPatternを使う
$windowPattern=$window.GetCurrentPattern([System.Windows.Automation.WindowPattern]::Pattern)
#ウィンドウサイズを変更する準備としてサイズを通常に変更する
$windowPattern.SetWindowVisualState([System.Windows.Automation.WindowVisualState]::Normal)
#ウィンドウサイズを変更するためのﾊﾟﾀｰﾝ。
$transformPattern=$window.GetCurrentPattern([System.Windows.Automation.TransformPattern]::Pattern)
#Maximamだと移動もｻｲｽﾞ変更もできないので注意。
$transformPattern.Resize($width,$heigh)
$transformPattern.Move($x,$y)
