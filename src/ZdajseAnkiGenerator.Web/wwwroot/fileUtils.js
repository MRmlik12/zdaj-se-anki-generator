export function saveFile(fileName, content) {
    return new Promise(() => {
        const link = document.createElement('a');
        link.download = fileName;
        link.href = URL.createObjectURL(new Blob([content], { type: 'application/octet-stream' }));
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    });
}
