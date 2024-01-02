# Unity Neovim CodeEditor Plugin

## Description

I got frustrated enough with Omnisharp that I implemented a Neovim extension for Unity Editor to hack my way around shortcomings of Omnisharp.
To work around [this issue](https://github.com/neovim/neovim/issues/14042) specifically.

Project generation directly uses `com.unity.ide.rider@3.0.27` project generation code, with very little modification.

This plugin uses nvim remote capabilities and uses the `/tmp/nvimsocket` file handler to communicate with nvim instance.

## What this plugin does

**This plugin does not do anything for code completion, syntax highlighting, refactoring etc. These are concerns for your Editor of choice (nvim?) leveraging information available in these project files.**

This plugin simply:
- Creates `.csproj` and `.sln` files so the LSP can work with code completion, syntax highlighting, refactoring.
- Updates project files as needed. Unity needs to be focused for this to trigger, so when you add a file focus Unity and let the plugin do its work.
- Sends a remote message to listening nvim instance for:
    1. Opening a file, with a specific line number
    2. Sending a :LspRestart when solution files are modified (a file is added/removed/modified that has an impact on the compilation)

## Usage

Make sure you have `nvim` executable in your `$PATH`. Plugin executes `which nvim` command to find the nvim executable and uses that executable to send commands through `/tmp/nvimsocket`.

**Your nvim instance should be started with `nvim --listen /tmp/nvimsocket`.**

## Installation

This package can be installed 
- directly through Unity `Package Manager` > `Add package from git URL` > `https://github.com/remzisenel/com.remzisenel.ide.neovim.git`
- or download or clone the root folder, use Unity Package Manager Window to add as a local package. 