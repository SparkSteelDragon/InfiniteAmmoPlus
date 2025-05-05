const fs = require('fs');
const path = require('path');

module.exports = {
  bump: (options) => {
    const version = options.version;

    // Update build.txt
    const buildTxtPath = path.join(__dirname, '..', 'build.txt');
    let buildContent = fs.readFileSync(buildTxtPath, 'utf8');
    buildContent = buildContent.replace(/version\s*=\s*.*/, `version = ${version}`);
    fs.writeFileSync(buildTxtPath, buildContent);
    console.log(`✅ Updated build.txt to version ${version}`);

    // Update InfiniteAmmoPlus.csproj
    const csprojPath = path.join(__dirname, '..', 'InfiniteAmmoPlus.csproj');
    let csprojContent = fs.readFileSync(csprojPath, 'utf8');

    // Match <Version>...</Version> or add if not exists
    if (csprojContent.includes('<Version>')) {
      csprojContent = csprojContent.replace(
        /<Version>.*<\/Version>/,
        `<Version>${version}</Version>`
      );
    } else {
      // Insert <Version> inside first <PropertyGroup>
      csprojContent = csprojContent.replace(
        /<PropertyGroup>([^]*)<\/PropertyGroup>/m,
        (match) => {
          return match.replace(
            '</PropertyGroup>',
            `  <Version>${version}</Version>\n</PropertyGroup>`
          );
        }
      );
    }
    fs.writeFileSync(csprojPath, csprojContent);
    console.log(`✅ Updated InfiniteAmmoPlus.csproj to version ${version}`);
  }
};