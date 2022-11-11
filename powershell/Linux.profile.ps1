# Alias
Set-Alias ll 'Get-ChildItem'

# Values
$ip = "133.18.209.234"
$provider      = "kagoya.jp"
$instance_name = "train"
$dist          = "alma"
$distver       = $(cat /etc/redhat-release).Split(" ")[2] 
$tag           = "dev"
$envtag     = "$tag-$dist-$distver"

# Prompt String
Function prompt{
    $git_status = $(git status -b 2> 1)
    $branch     = ""
    if("$git_status" -ne ""){
        $branch   = "(" + $($git_status[0].Split(" ")[2]) + ")"
    }
    $hostname = $(hostname)   
    $current  = $(Split-Path $(Get-Location) -Leaf)

    "[${provider}:${instance_name} ${current}${branch}]$ "
}

Function Add-GitTag{
    Param(
#       [Parameter(
#           Mandatory,
#           HelpMessage="タグ名"
#       )]
        [ValidateScript({
            $_ -match '^(dev|stg|prod)-(alma|rhel|ubuntu|cent|alpine|rocky)-[0-9]+\.[0-9]+'
        })]
        [string]$Name
        ,
#        [Parameter(
#           Mandatroy,
#           HelpMessage="タグのコメント"
#       )]
        [string]$Comment
    )
    git tag -a $Name -m $Comment 

}
