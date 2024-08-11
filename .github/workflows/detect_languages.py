import os

# Map file extensions to CodeQL-supported languages
EXTENSION_LANG_MAP = {
    '.py': 'python',
    '.js': 'javascript',
    '.java': 'java',
    '.cpp': 'cpp',
    '.c': 'cpp',
    '.cs': 'csharp',
    '.go': 'go',
    '.rb': 'ruby',
    '.php': 'php',
    '.swift': 'swift',
    '.kt': 'java',  # CodeQL supports Kotlin under Java
    '.rs': 'cpp',   # CodeQL supports Rust under C++
    '.ts': 'typescript',
    # Add more mappings as needed
}

def detect_languages():
    detected_languages = set()
    
    for root, dir, files in os.walk('.'):
        for file in files:
            ext = os.path.splitext(file)[1]
            if ext in EXTENSION_LANG_MAP:
                detected_languages.add(EXTENSION_LANG_MAP[ext])
    # write detected languages as csv
    return ','.join(detected_languages)


if __name__ == "__main__":
    languages = detect_languages()
    print(languages)
