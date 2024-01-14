using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace NeovimEditor.Editor {
    public class NeovimFileWatcher {
        public event Action<string[]> FilesModified;

        private FileSystemWatcher _watcher;
        private bool _fileAdded = false;
        private List<string> _filesToSync = new();
        private SemaphoreSlim _semaphore = new(1, 1);

        public NeovimFileWatcher() {
            _watcher = new FileSystemWatcher() {
                Path = Application.dataPath,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName,
                Filter = "*.cs",
                EnableRaisingEvents = true
            };

            _watcher.Created += OnFileCreated;
            _watcher.Deleted += OnFileDeleted;
            _watcher.Renamed += OnFileRenamed;

            EditorApplication.update += Update;
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e) {
            var relativePath = e.FullPath.Replace(Application.dataPath, "Assets");
            FileUpdated(relativePath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e) {
            var relativePath = e.FullPath.Replace(Application.dataPath, "Assets");
            FileUpdated(relativePath);
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e) {
            var relativePath = e.FullPath.Replace(Application.dataPath, "Assets");
            FileUpdated(relativePath);
        }

        private void FileUpdated(string fileName) {
            _semaphore.Wait();
            if (!_filesToSync.Contains(fileName)) {
                _filesToSync.Add(fileName);
            }
            _fileAdded = true;
            _ = _semaphore.Release();
        }

        private void Cleanup() {
            _watcher.Created -= OnFileCreated;
            EditorApplication.update -= Update;
            _watcher.Dispose();
        }

        private void Update() {
            if (_fileAdded) {
                _semaphore.Wait();
                _fileAdded = false;
                FilesModified?.Invoke(_filesToSync.ToArray());
                _filesToSync.Clear();
                _ = _semaphore.Release();
            }
        }
    }
}