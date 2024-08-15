1. Open https://pastebin.com/ or a similar service in any browser.
2. Create 'New Paste' with the following attributes:
   * Code:
            git config --global user.name  "New Sheriff in Town"
            git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
            git push origin master --force
   * Syntax Highlighting: "Bash"
   * Paste Expiration: "10 Minutes"
   * Paste Name / Title: "how to gain dominance among developers"
3. Save 'paste' and check the following:
   * Browser page title matches 'Paste Name / Title'
   * Syntax is suspended for bash
   * Check that the code matches the one from paragraph 2
