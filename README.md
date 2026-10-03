# MT2Config

[![Build](https://github.com/MT2Dev/MT2Config/actions/workflows/build.yml/badge.svg)](https://github.com/MT2Dev/MT2Config/actions/workflows/build.yml)

MT2Config is the `config.exe` of a Metin2 game client: the small Windows dialog players open to choose their
resolution, graphics, sound and interface options before starting the game.

It reads and writes `metin2.cfg` with exactly the same rules as the client's own `CPythonSystem::LoadConfig()` and
`SaveConfig()`. It never breaks or deletes the file the client writes, and it is easy to adapt to your own server.

## Contents

- [Why](#why)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Options](#options)
- [How metin2.cfg is handled](#how-metin2cfg-is-handled)
- [Customization](#customization)
- [Building](#building)
- [Releases](#releases)
- [Project structure](#project-structure)
- [Troubleshooting](#troubleshooting)
- [License and credits](#license-and-credits)

## Why

Config tools shared for Metin2 usually treat `metin2.cfg` as their own file. Typical problems:

- They reject the keys the client itself writes (`OBJECT_CULLING`, `SAVE_ID`, ...) as "corrupted" and reset the whole
  file. The player loses their settings and their saved login ID.
- They replace `.` with `,` and then parse numbers with the Windows culture. On an English system `0.5` becomes `5`,
  the volume slider gets an out-of-range value and the tool crashes.
- They write volumes in a format the client misreads. For example, `MUSIC_VOLUME 1` is the old 0-5 scale, about
  16% volume.

MT2Config is built around the client's code instead. Whatever the client writes, the tool reads the same way.
Whatever the tool writes, the client reads back exactly as shown in the dialog.

## Features

- **Client-compatible `metin2.cfg` handling.** The file is never deleted. Keys the tool does not edit are kept,
  numbers are written in the client's format, and missing keys mean the client's defaults.
  See [How metin2.cfg is handled](#how-metin2cfg-is-handled).
- **Only options that matter.** Settings the client reads but never applies are hidden, and their values are kept
  (see [Options the client does not apply](#options-the-client-does-not-apply)).
- **Real display modes.** Resolutions and refresh rates come from the primary monitor (`EnumDisplaySettings`, 32-bit
  modes of at least 800 × 600), not from a fixed list. A value the list does not offer is kept and marked *(custom)*.
- **Refresh rate limit.** At most 60 Hz, the highest rate the client supports. Faster monitor modes are not offered,
  and a higher `FREQUENCY` in `metin2.cfg` is lowered.
- **13 languages.** English, Turkish, Danish, German, Spanish, French, Italian, Hungarian, Dutch, Portuguese,
  Romanian, Greek and Russian. The language follows Windows and can be changed in the window.
- **Tooltips.** Pointing at an option explains exactly what it does, in the selected language.
- **Dark mode.** One check box switches the window at once. It follows the Windows app mode on first start. On
  Windows 10 (1809+) and 11 the title bar turns dark too. High contrast themes keep their system colors.
- **"Save and Play".** Saves and starts the client, shown only when the client executable is next to `config.exe`.
- **A single small file.** Players only need `config.exe`.
  - It runs on Windows 7 SP1, 8.1, 10 and 11.
  - It is sharp at the display scaling of the main monitor (125%, 150%, ...).
  - It needs no administrator rights.

## Requirements

| | Needed |
|---|---|
| Players | Windows 7 SP1, 8.1, 10 or 11 with .NET Framework 4.8. It is built into Windows 10 (1903+) and Windows 11. On older versions it usually comes through Windows Update; otherwise use [Microsoft's installer](https://dotnet.microsoft.com/download/dotnet-framework/net48). Windows 8 (not 8.1) cannot run .NET Framework 4.8. |
| Building | Visual Studio 2026 with the ".NET desktop development" workload, or the .NET SDK on Windows. |

## Installation

1. Download `config.exe` from the [Releases](https://github.com/MT2Dev/MT2Config/releases) page, or adapt the
   project to your server (see [Customization](#customization)) and build it (see [Building](#building)).
   - The release and the `config` artifact of this repository's
     [GitHub Actions runs](https://github.com/MT2Dev/MT2Config/actions/workflows/build.yml) carry this repository's
     version information and client executable names.
   - In a fork, GitHub keeps the workflows disabled until you enable them once in the **Actions** tab.
2. Put `config.exe` into the client folder, next to the client executable and `metin2.cfg`.
   - The tool always edits the `metin2.cfg` in the folder that contains `config.exe`, whatever its working directory
     is (for example when a patcher starts it).
   - The client reads `metin2.cfg` from its working directory. That is its own folder when it is started normally or
     through **Save and Play**.
3. Ship only `config.exe` to players. `config.exe.config` and `config.pdb` from the build folder are optional.
   - With `config.exe.config` next to it, Windows offers to install .NET Framework 4.8 when it is missing.
   - Without it, Windows does not check the version, so the tool may also start on older .NET Framework 4.x
     versions. This is not tested; 4.8 is the supported version.

If the game is installed under `Program Files`, Windows only lets administrators write there. The tool then shows a
message asking to run it as administrator. Installing the game outside `Program Files` avoids this. The client
has the same limitation, since it writes `metin2.cfg` too.

## Options

Every game option in the window is stored in `metin2.cfg` and is read and written as the client does. Resolution
uses two keys (`WIDTH` and `HEIGHT`); every other option uses one. Language and Dark mode only affect this window,
see [Settings of the tool itself](#settings-of-the-tool-itself). The tooltips in the program contain the same
explanations as this table.

| Option | Key | Values | Default | What it does |
|---|---|---|---|---|
| Screen mode | `WINDOWED` | 0 fullscreen, 1 windowed | 0 | Fullscreen switches the monitor to the chosen resolution. Windowed runs the game in a normal window of that size. |
| Resolution | `WIDTH`, `HEIGHT` | pixels | 1024 × 768 | Size of the game picture. In windowed mode the client never makes the window larger than the screen. |
| Refresh rate | `FREQUENCY` | Hz, at most 60 | 60 | Refresh rate requested for fullscreen mode. Not used (and greyed out) in windowed mode. Many clients ignore it and keep the monitor's own rate; see the note below. |
| Shadows | `SHADOW_LEVEL` | 0 off, 1 ground, 2 ground and own character, 3 all, 4 all (high), 5 all (maximum) | 3 | Which shadows are drawn. Levels 4 and 5 use higher resolution shadow textures and need a stronger graphics card. |
| Terrain tiling | `SOFTWARE_TILING` | 0 auto, 1 CPU, 2 GPU | 0 | How ground textures are drawn. Auto picks the method that suits the graphics card. |
| Software cursor | `SOFTWARE_CURSOR` | 0 / 1 | 0 | The game draws the mouse cursor itself instead of Windows. |
| Music | `MUSIC_VOLUME` | 0.000 - 1.000 | 1.000 | Background music volume, shown as 0 - 100%. |
| Effects | `VOICE_VOLUME` | 0 - 5 | 5 | Sound effects volume, shown as 0 - 100% in steps of 20%. |
| Show chat | `VIEW_CHAT` | 0 / 1 | 1 | Shows the chat messages of players in the game. |
| Always show names | `ALWAYS_VIEW_NAME` | 0 / 1 | 1 | Always shows the names of nearby characters, monsters, NPCs and items on the ground. When off, most names appear only when pointed at or while Alt is held. |
| Show damage | `SHOW_DAMAGE` | 0 / 1 | 1 | Shows damage numbers above the target you hit and above your own character when you are hit. |
| Show shop titles | `SHOW_SALESTEXT` | 0 / 1 | 1 | Shows the title signs of players' private shops. |
| Use Windows IME | `USE_DEFAULT_IME` | 0 / 1 | 0 | Lets Windows show the IME candidate list (word choices while typing Chinese, Japanese or Korean) instead of the game's own list. Typing works either way. |

**Refresh rate note.** In the vanilla client, builds for the European locale (the usual case) show fullscreen as a
borderless window on a resolution switch (`PythonApplication.cpp`, the `LocaleService_IsEUROPE()` branch in
`Create()`). They do not set a refresh rate, so Windows keeps the monitor's own rate. The tool still writes the
value, so a client with real exclusive fullscreen can use it.

These keys are kept as they are, because the tool has no option for them: `BPP`, `IS_SAVE_ID`, `SAVE_ID`,
`PRE_LOADING_DELAY_TIME`, `NO_SOUND_CARD`, and any key a modified client adds. The keys of the hidden options below
are kept the same way.

The buttons:

- **Defaults** puts the client's default values into the game settings of the window. Language and Dark mode are
  not changed, and nothing is saved until **Save** or **Save and Play** is clicked.
- **Save** writes `metin2.cfg` and closes the window.
- **Save and Play** also starts the client.
- **Cancel** closes the window without saving `metin2.cfg`. Language and Dark mode changes are remembered as soon as
  they are made.

### Options the client does not apply

The vanilla client reads these keys from `metin2.cfg` and saves them again, but never uses them. They are therefore
hidden in the window, and their lines in `metin2.cfg` are not touched at all, also not by the **Defaults** button.
If your client has been changed to use one of them, show it again in `GameClient.cs`
(see [Server settings](#server-settings-gameclientcs)).

| Option | Key | Values | Default | Intended use | Shown with |
|---|---|---|---|---|---|
| Gamma | `GAMMA` | 0 - 5 | 3 | Brightness (gamma) correction of the game picture. | `ShowGamma` |
| View distance | `VISIBILITY` | 1 near, 2 medium, 3 far | 3 | How far the world is drawn before it fades into fog. The vanilla client uses a fixed view distance. | `ShowViewDistance` |
| Object culling | `OBJECT_CULLING` | 0 / 1 | 1 | Skip drawing objects that are out of view. | `ShowObjectCulling` |
| Uncompressed textures | `DECOMPRESSED_TEXTURE` | 0 / 1 | 0 | Load DXT textures uncompressed. The vanilla client decompresses them by itself when the graphics card lacks DXT support. | `ShowDecompressedTextures` |

## How metin2.cfg is handled

`ClientConfig.cs` mirrors the client's reading and writing code. These are the rules it follows.

**Reading**

- **Keys** are case-insensitive, like `stricmp`. The comparison is ordinal, so Turkish `i`/`İ` casing cannot break it.
- **Values** are read with the same rules as the client's `atoi`/`atof`. `"12abc"` is 12, and decimals always use `.`
  whatever the Windows language is.
- **`MUSIC_VOLUME` without a `.`** uses the old 0-5 scale, as the client does: `5` is 100%, `1` is about 16%.
- **The first empty line ends the file**, as it does in the client (`sscanf` returns `EOF`). Lines after it are
  ignored and are not written back.
- **A line with only a key** reuses the previous line's value, as `sscanf` does in the client.
- **Missing keys** take the client's `SetDefaultConfig()` values. This matters because `SaveConfig()` leaves
  `WINDOWED`, `VIEW_CHAT`, `ALWAYS_VIEW_NAME`, `SHOW_DAMAGE` and `SHOW_SALESTEXT` out of the file while they hold
  their default value.
- **A UTF-8 BOM** at the start (left by some text editors) is ignored. In the client it would break the first key.

**Values the list does not offer**

- They are kept and marked *(custom)*, for example a resolution of another monitor.
- `FREQUENCY` is the exception. A rate above 60 Hz, or 0/1 (monitor default), is replaced by the highest supported
  rate. A custom rate is also replaced when another resolution is chosen.

**Writing**

- Keys are written in the order of `SaveConfig()`, followed by any other keys in their original order. Every key of
  an option shown in the window is written explicitly; the keys of hidden options are left as they are.
- `MUSIC_VOLUME` is written with three decimals (`%.3f`), so the client never mistakes it for the old scale.
  `VOICE_VOLUME` is the client's 0-5 integer.
- Lines end with CRLF. The only empty line is the one at the end, as `SaveConfig()` writes it.
- Values the tool does not change, `SAVE_ID` included, are written back unchanged, byte for byte.
- A key that has no value is written with the value the client also reads for it (see "A line with only a key"). The
  client's empty `SAVE_ID` line therefore becomes `SAVE_ID 0`, which the client reads the same way.

## Customization

Everything server-specific is kept in a few small places.

### Server settings: `GameClient.cs`

```csharp
// Window title: "Metin2 - Settings".
public const string Name = "Metin2";

// Highest refresh rate the client supports.
public const int MaxRefreshRate = 60;

// Options the client reads but never applies: hidden, their lines in metin2.cfg are not touched.
public static readonly bool ShowGamma = false;
public static readonly bool ShowViewDistance = false;
public static readonly bool ShowObjectCulling = false;
public static readonly bool ShowDecompressedTextures = false;

// "Save and Play" starts the first of these found next to config.exe.
static readonly string[] ExecutableNames = { "MT2DevCore.exe", "metin2client.exe", "Metin2Release.exe", "Metin2Distribute.exe" };
```

- Put your client's file name first in `ExecutableNames`.
- If players must start the game through a patcher, empty the list. The "Save and Play" button is then hidden.
- Set a `Show...` flag to `true` once your client really applies that option
  (see [Options the client does not apply](#options-the-client-does-not-apply)).
- The tooltips of the refresh rate and of "Save and Play" show these values automatically.

### Default values: `ClientConfig.cs`

The property initializers (`Width = 1024`, `Windowed = false`, `MusicVolume = 1.0f`, ...) serve two purposes:

- They are the values used for keys missing from `metin2.cfg`.
- They are what the **Defaults** button shows.

They must match your client's `CPythonSystem::SetDefaultConfig()` and `DEFAULT_VALUE_ALWAYS_SHOW_NAME`. Otherwise a
file the client wrote is misread, because `SaveConfig()` omits the five keys listed above while they hold their
default value. If you change a default in the client, change it here as well. The Gamma tooltip (when Gamma is shown)
displays the `Gamma` default automatically.

### Option lists: `MainForm.cs`

`VisibilityValues`, `ShadowValues`, `TilingValues` and `GammaValues` are the values the drop-down lists offer. If
your client supports a different range, change them here.

- **Item names** come from the `ViewDistances` (index = value - 1), `ShadowLevels` and `TilingModes` (index = value)
  arrays of every `Languages/*.cs` file. A value without a name is shown as its number.
- **Tooltips:** `TipViewDistance`, `TipShadows` and `TipTiling` describe the shipped levels. Update them in every
  language file if you change the levels.

### Resolution list: `DisplayModes.cs`

- **Smallest resolution:** `MinWidth` and `MinHeight` (800 × 600) are the smallest resolution offered. Raise them if
  your client's interface needs more, for example 1024 × 768.
- **Fallback list:** `FallbackResolutions` is offered when Windows cannot report the monitor's modes.

### File version information: `MT2Config/MT2Config.csproj`

`AssemblyTitle` (file description), `Company`, `Product`, `Copyright` and `Version` are what Windows shows in the
file properties of `config.exe`.

The exe icon and the window icon both come from `MT2Config/app.ico`. Replace that file to use your own; keep
several sizes in it (16, 24, 32, 48 and 256 pixels) so it looks sharp everywhere.

### Dark theme colors: `Theme.cs`

`Theme.Dark` holds the colors of the dark mode:

- **Background:** the window.
- **Surface:** inputs, buttons and tooltips. **SurfaceHover** and **SurfacePressed** are used while they are pointed
  at or pressed.
- **Border:** borders of inputs, buttons, group boxes and tooltips. **BorderHover** is used while pointed at.
- **Text** and **DisabledText:** normal and greyed-out text.
- **Accent:** focus border and checked boxes. **AccentText** is the check mark on it.

The light theme uses the native Windows look and has no colors of its own.

### Languages: `Language.cs` and `Languages/*.cs`

Each language is one file in `Languages/` with a factory method such as `CreateGerman()`. To add a language:

1. Copy `Languages/English.cs` and rename the file and the method, for example `Languages/Polish.cs` with
   `CreatePolish()`.
2. Set `Code` to the two-letter ISO code (`"pl"`) and `Name` to the language's own name (`"Polski"`).
3. Translate every text, the `Tip*` tooltips included. Keep `{0}`/`{1}` placeholders and line breaks, and do not use
   `&`. Tooltips need no line breaks; they are wrapped automatically.
4. In `Language.cs`, add `public static readonly Language Polish = CreatePolish();` next to the other language
   fields, **above** the `All` array. Static fields are initialized in order, so a field below `All` would still be
   empty when the list is built.
5. Put `Polish` into the `All` array, which is also the order of the language list.

Language selection works like this:

- **First start:** the language is chosen from the Windows display language, then from the regional format, then
  English.
- **Later starts:** the player's choice is remembered.
- **Packaging:** all languages are compiled into `config.exe`; there are no satellite resource DLLs.

### Adding a new metin2.cfg option

If your client reads and writes an extra key in `LoadConfig()`/`SaveConfig()`:

1. In `ClientConfig.cs`:
   - add a property with the client's default value,
   - read it in `ReadEntries()` with the same conversion the client uses (`atoi`, `atoi(...) == 1`, ...),
   - write it in `WriteEntries()`,
   - add the key to `SaveOrder`.
2. In the designer (`MainForm.Designer.cs`), add a control to one of the groups. Use `ThemedCheckBox` or
   `ThemedComboBox` so it follows the dark mode.
3. In `MainForm.cs`, connect it in `ShowConfig()` and `ReadConfig()`, set its text in `ApplyLanguage()`, and add its
   tooltip in `ApplyTips()`.
4. Add the label and tooltip texts as properties in `Language.cs` and translate them in every `Languages/*.cs` file.

A key you do not want to show in the window needs no code at all. Unknown keys are preserved, as long as they come
before the first empty line.

### Settings of the tool itself

The language and dark mode choices are stored per Windows user in the registry under `HKCU\Software\MT2Config`
(values `Language` and `DarkMode`). No administrator rights are needed, and `metin2.cfg` is not touched by them.

## Building

The project is an SDK-style C# project targeting .NET Framework 4.8. The assembly name is `config`, so the output is
`config.exe`.

**Visual Studio 2026**

1. Install the ".NET desktop development" workload.
2. Open `MT2Config.slnx`.
3. Build the Release configuration.

The output is `MT2Config\bin\Release\config.exe`. The form can be edited in the Windows Forms designer.

When you edit the form, do not add images or icons to it, for example through the form's `Icon` property. The designer
would store them in a `.resx` file, and `dotnet build` (also used by CI) cannot embed such resources for .NET Framework
projects without extra packages. That is why the icon is the embedded resource `app.ico`.

**Command line (Windows)**

```
dotnet build MT2Config.slnx -c Release
```

The `.slnx` solution needs the .NET SDK 9.0.200 or later. With an older SDK, build `MT2Config/MT2Config.csproj`
directly.

**Continuous integration**

`.github/workflows/build.yml` builds every push to a branch and every pull request on Windows, and uploads
`config.exe` as an artifact.

## Releases

`.github/workflows/release.yml` builds `config.exe` with the release version (file version `X.Y.Z.0`) and creates a
**draft** release with `config.exe` attached.

1. Optionally write the release notes into `.github/release-notes/vX.Y.Z.md`, for example
   `.github/release-notes/v1.1.0.md`. A `{SHA256}` placeholder in it is replaced with the checksum of `config.exe`.
   Without a notes file, the release only lists the checksum.
2. Start the workflow in one of two ways:
   - **Actions tab:** choose **Release**, then **Run workflow**, and enter the version (for example `1.1.0`). The tag
     `v1.1.0` is created on the built commit when you publish the draft.
   - **Tag:** push a tag of the form `vX.Y.Z`:

     ```
     git tag -a v1.1.0 -m "MT2Config 1.1.0"
     git push origin v1.1.0
     ```

3. Check the draft on the [Releases](https://github.com/MT2Dev/MT2Config/releases) page and click **Publish release**.

GitHub attaches the source code archives of the tag to every published release, which also covers the GPL
requirement to make the source of the shipped version available.

## Project structure

```
MT2Config.slnx                 Solution
MT2Config/
  MT2Config.csproj             Project, file version information
  app.manifest                 Windows 7-11 compatibility, DPI awareness, no elevation
  app.ico                      Exe and window icon
  Program.cs                   Entry point, global error handler
  MainForm.cs                  Window logic: options <-> ClientConfig, languages, tooltips, theme
  MainForm.Designer.cs         Window layout (Windows Forms designer)
  ClientConfig.cs              metin2.cfg model, mirrors CPythonSystem::LoadConfig/SaveConfig
  CRuntime.cs                  atoi/atof with the client's C runtime rules
  DisplayModes.cs              Resolutions and refresh rates of the monitor
  GameClient.cs                Server-specific settings
  Language.cs                  Text properties, language list and selection
  Languages/*.cs               One file per language
  Theme.cs                     Light/dark theme colors and preference
  ThemedControls.cs            Combo box, check box and group box with dark drawing
  UserSettings.cs              The tool's own settings in HKCU
  NativeMethods.cs             Win32 declarations
.github/workflows/build.yml    GitHub Actions build of every push and pull request
.github/workflows/release.yml  Draft release with config.exe (Actions tab or vX.Y.Z tag)
.github/release-notes/         Release notes per tag
```

## Troubleshooting

### "The settings could not be saved"

The folder is not writable. This usually happens when the game is under `Program Files`. Run `config.exe` as
administrator, or install the game somewhere else.

### The program does not start on Windows 7

Install .NET Framework 4.8; Windows 7 needs Service Pack 1 for it.

### A value is shown as "(custom)"

`metin2.cfg` contains a value the list does not offer, for example a resolution of another monitor. It is kept
unless you choose another value. A custom refresh rate is also replaced when you choose another resolution.

### The refresh rate is greyed out

It is only used in fullscreen mode.

### Antivirus or SmartScreen warnings

Small unsigned executables are sometimes flagged. Signing `config.exe` with a code-signing certificate reduces this.
SmartScreen can still warn until the signed file has built up reputation, and false positives can be reported to the
antivirus vendor.

## License and credits

MT2Config is licensed under the [GNU General Public License v3.0](LICENSE). The original version was written by
Takuma (work.takuma@gmail.com). This fork rewrites it for client compatibility and a modern toolchain, and adds the
languages, dark mode and tooltips.

If you distribute `config.exe`, for example with your game client, the GPL requires two things:

- The people who receive it must be able to get the source code of that exact version, including your changes.
- They must also receive the license text.

A public fork on GitHub that contains the shipped version is a simple way to provide the source. Tell players where
to find it, for example with a link on your download page, in your launcher or in a text file next to `config.exe`.
