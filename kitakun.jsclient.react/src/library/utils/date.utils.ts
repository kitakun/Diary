export function formatDate(date: Date, format: string): string {
    switch (format) {
        case 'dd.MM.yyyy':
            return formatDate_dd_MM_yyyy(date);
        default:
            console.warn(`Format ${format} not implemented!`);
            return `[NOT IMPLEMENTED]`;
    }
}

export function formatDate_dd_MM_yyyy(date: Date): string {
    const monthIndex = date.getMonth();
    const month = monthIndex < 9 ? `0${monthIndex + 1}` : monthIndex + 1;
    const day = String(date.getDate()).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
}