# Change Log:

## 0.0.2

- Added a file system watcher, it triggers an AssetDatabase.Refresh() when a file is modified in Application.dataPath, removing the need to Alt-Tab to Unity when files are modified/a new file is created/a file is
  removed. This might increase energy consumption as any write to dataPath triggers an AssetDatabase.Refresh() - which inherently is quite a costly operation.