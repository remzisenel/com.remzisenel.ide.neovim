# Change Log:

## 0.0.3

- Removed LspRestart command that was triggered on project sync as this is no longer needed in my setup. I'm not sure what caused this change if omnisharp finally fixed it or not but it appears to be working fine without this, if for whatever reason your configuration doesn't work as expected, you can enable this behaviour by uncommenting lines 26:30 in NvimIntegration.cs

## 0.0.2

- Added a file system watcher, it triggers an AssetDatabase.Refresh() when a file is modified in Application.dataPath, removing the need to Alt-Tab to Unity when files are modified/a new file is created/a file is
  removed. This might increase energy consumption as any write to dataPath triggers an AssetDatabase.Refresh() - which inherently is quite a costly operation.