# ssh config

```
Property              Description
--------------------- -------------------------------------------------------------------------
Host                  接続するホストのエイリアス
HostName              IPアドレス または ホスト名
User                  ログインユーザ名
IdentityFile          秘密鍵のパス
Port                  ポート番号 ※ デフォルトは 22 番
IdentitiesOnly 	      指定した秘密鍵のみを使用するかどうか（yes, no）
TCPKeepAlive          Keepalive の有効 / 無効（yes, no）※ デフォルトは yes
ServerAliveInterval 	KeepAlive メッセージの確認間隔（秒）
ServerAliveCountMax 	サーバから応答がなかった場合に、KeepAlive メッセージを何回まで送るか（回数）
ForwardAgent          認証エージェントへの接続を、接続先に転送するかどうか（yes, no）※ デフォルトは no
ProxyCommand          ProxyCommand を指定する
```



### Example

```
# 198.51.100.10 のサーバ
Host 198.51.100.10
    User hoge
    Port 22222
    IdentityFile ~/.ssh/id_rsa
```
```
# example.com のサーバ
Host example
    HostName example.com
    User fuga
    IdentityFile ~/.ssh/piyo.pem
    IdentitiesOnly  yes
```
