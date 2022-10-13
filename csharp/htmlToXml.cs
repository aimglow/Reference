
HOME >  エンジニア >
エンジニア
C#のLINQ to XMLでHTMLを読む
投稿日：2016年4月13日 更新日：2020年1月22日
今回のエンジニアブログを担当する加賀です。

個人的にXML、HTMLを扱う機会があったので、LINQ to XMLを使用してみました。

LINQ to XMLは.NET Framework 3.5以降で使用できます。
System.Xml.Linq名前空間の各クラスで構成されています。
XML文書へのアクセスを簡単に行う事が出来ます。

しかし、HTMLはそのままではLINQ to XMLでは扱えない（エスケープ文字でXMLにないものを使っているなど）ため、一度変換する必要があります。
今回はSGMLReaderというライブラリを使ってHTMLをXMLに変換することで、LINQ to XMLで使用できる形にします。

HTML to XML

Sgml.SgmlReaderは、XmlReaderを継承しているので、最初に読み込む部分を作成してしまえば、後はLINQ to XMLでそのまま使う事が出来ます。

XDocument ConvertToXML(Stream stream) {
  StreamReader sr = new StreamReader (stream);
  XDocument xml = null;
  // SGMLReaderの作成  
  using(SgmlReader sgml = new SgmlReader ()) {
    // DTDを取得しにいかないように設定  
    sgml.IgnoreDtd = true;
    // ドキュメントタイプをHTMLに設定  
　　sgml.DocType = "HTML";
    // 読み込むストリームの設定  
　　sgml.InputStream = sr;
    // XMLに変換  
    xml = XDocument.Load (sgml);
  }
  sr.Close ();

  return xml;
}
たったこれだけで、HTMLからXMLに変換できます。

LINQ to XMLでデータを検索

LINQ to XMLでのデータ検索は、上手に絞り込みをするとかなり簡潔に書けます。
HTMLの場合はソースを見ることがしやすく、ブラウザによっては指定のタグがどの範囲に描画されているかを見ながら探す事が出来るので、それらを使うことで絞り込みのヒントが得やすくなります。
また、サイトによっては特に重要な情報を含むタグやその親に、一意の名前を付けていることが多いので、簡単に目当ての情報を取得できます。

検索に使用できるメソッドはXElementを参照してください。

今回は例として、Twitter(PC版)の@CrashFever_PR(クラッシュフィーバー公式)のスクリーン名(表示ユーザー名)と自己紹介文の取得をしてみます。
今回取得するURLは、https://twitter.com/CrashFever_PRです。
例なのでエラー処理はありません。（あるとしても今回はnullチェックくらいです）

XDocument xml = ConvertToXML (事前に取得したデータのStream);
// スクリーン名の取得  
// 場所：ProfileHeaderCard-nameクラスのh1タグ　内のaタグの内容  
// Descendants(子方向探索)で h1 タグのみに絞り込む  
// Whereで ProfileHeaderCard-name というクラス指定を持つ h1 タグに絞り込む  
// Selectで h1 タグ内の最初の a タグを取得(今回は a タグは1つしか存在しない)  
// FirstOrDefaultで1つだけ取得  
// 取得したXElementのValueがスクリーン名  
XElement screenNameElement = xml
    .Descendants ("h1")
    .Where (h1 =>
        h1.Attribute ("class") != null &&
        h1.Attribute ("class").Value == "ProfileHeaderCard-name")
    .Select (h1 => h1.Element ("a"))
    .FirstOrDefault ();
string screenName = screenNameElement.Value;

// 自己紹介文の取得  
// 場所：ProfileHeaderCard-bioクラスのpタグの内容  
// Descendants(子方向探索)で p タグのみに絞り込む  
// Whereで ProfileHeaderCard-bio というクラス指定を持つ p タグに絞り込む  
// FirstOrDefaultで1つだけ取得  
// 取得したXElementのValueが自己紹介文  
XElement textElement = xml
    .Descendants ("p")
    .Where (p =>
        p.Attribute ("class") != null &&
        p.Attribute ("class").Value.Contains ("ProfileHeaderCard-bio")) // クラス指定が複数されている場合はContainsで判定  
    .FirstOrDefault ();
string text = textElement.Value;
いちいちルートから辿って行ったり、全文検索で探したりしなくても、簡単に検索ができます。
応用すればかなり複雑な絞り込みもできます。

終わり

今回、はじめてLINQ to XMLを使用しましたが、非常に簡単にデータを取得することが出来ました。
XMLベースのAPIの値を取得するのもいいですし、今回のようにHTMLページからデータを取得することも簡単です。
RSSのないページの更新検知ソフトとか簡単に作れそうです。

採用情報

ワンダープラネットでは、一緒に働く仲間を幅広い職種で募集しております。
採用情報 | ワンダープラネット
Wantedly

-エンジニア
-C#

関連記事

 
エンジニア
AWS Session ManagerのSSH接続を導入した事例紹介
 
エンジニア
エラーにならなくなったLINQメソッド(Unity iOS)
 
エンジニア
CocoStudioを触ってみる（Data Editor編）
 
エンジニア
【Git】SourceTreeでのリベース手順
 
エンジニア
MacでC#を書いてみよう(実践編)
PREV
画像処理ライブラリ「Pillow」をAWS Lambdaで使ってみる
NEXT
【Unity】AESでデータを暗号化
 WonderPlanet Developers’ Blog
 © WonderPlanet Inc.