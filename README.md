# Byte Reader
A Windows command line tool that can dump specific parts of a file (on a byte by byte level) to console in both Hexadecimal and UTF8 format.

## Features
- The user can choose to skip to a specific section in the file by giving the amount of bytes they wish to skip.
- The byte address of each read block (in the file) is shown while reading.
- Hexadecimal and UTF8 printing of data.
- When the same byte occurs in sequence in a block, it is abbreviated to a single instance of that byte with the amount of occurances in front for readability.
- Custom read buffers to increase or decrease the amount of bytes read and shown at once.
- Support for very large files. Tested up to 20GB files.

## But why?
It is designed with the purpose of being able to easily read specific parts of very large files. Files commonly too large for regular text editors.

An example of such files are VM hard disk files.
