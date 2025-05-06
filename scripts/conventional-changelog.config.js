const compareFunc = require('compare-func');

module.exports = {
  writerOpts: {
    transform: (commit, context) => {
      const allowedTypes = ['feat', 'fix', 'perf'];

      if (!allowedTypes.includes(commit.type)) {
        return;
      }

      // create a new commit object to avoid mutating the original one
      const newCommit = { ...commit };

      // rename the commit type to a more human-readable format
      if (newCommit.type === 'feat') {
        newCommit.type = 'Features';
      } else if (newCommit.type === 'fix') {
        newCommit.type = 'Bug Fixes';
      } else if (newCommit.type === 'perf') {
        newCommit.type = 'Performance Improvements';
      }

      if (newCommit.scope === '*') {
        newCommit.scope = '';
      }

      // delete hash and references to avoid showing them in the changelog
      newCommit.hash = '';
      newCommit.shortHash = '';
      newCommit.references = [];

      return newCommit;
    },
    groupBy: 'type',
    commitGroupsSort: 'title',
    commitsSort: ['scope', 'subject'],
    noteGroupsSort: 'title',
    mainTemplate: `{{> header}}

{{#each commitGroups}}
### {{title}}

{{#each commits}}
* {{#if scope}}{{scope}}: {{/if}}{{subject}}
{{/each}}

{{/each}}
{{> footer}}`,
    headerPartial: `## {{version}} ({{date}})`,
    commitPartial: `* {{#if scope}}{{scope}}: {{/if}}{{subject}}`,
    footerPartial: '',
    compareFunc: compareFunc,
  },
  parserOpts: {
    headerPattern: /^(\w*)(?:\((.*)\))?: (.*)$/,
    headerCorrespondence: ['type', 'scope', 'subject'],
    noteKeywords: ['BREAKING CHANGE'],
    revertPattern: /^(?:Revert|revert:)\s"?([\s\S]+?)"?\s*This reverts commit (\w*)\./i,
    revertCorrespondence: ['header', 'hash'],
  },
};