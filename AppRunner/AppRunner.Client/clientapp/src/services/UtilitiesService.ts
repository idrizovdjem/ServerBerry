const invertColor = (colorHex: string, bw: boolean): string => {
    if (colorHex.indexOf('#') === 0) {
        colorHex = colorHex.slice(1);
    }

    if (colorHex.length === 3) {
        colorHex = colorHex[0] + colorHex[0] + colorHex[1] + colorHex[1] + colorHex[2] + colorHex[2];
    }

    const redColor: number = parseInt(colorHex.slice(0, 2), 16);
    const greenColor: number = parseInt(colorHex.slice(2, 4), 16);
    const blueColor: number = parseInt(colorHex.slice(4, 6), 16);

    if (bw) {
        return (redColor * 0.299 + greenColor * 0.587 + blueColor * 0.114) > 186
            ? '#000000'
            : '#FFFFFF';
    }

    const paddedRed: string = padZero((255 - redColor).toString(16));
    const paddedGreen: string = padZero((255 - greenColor).toString(16));
    const paddedBlue: string = padZero((255 - blueColor).toString(16));

    return `#${paddedRed}${paddedGreen}${paddedBlue}`;
}

const padZero = (color: string, length: number = 2): string => {
    const zeros = new Array(length).join('0');
    return (zeros + color).slice(-length);
}

// eslint-disable-next-line import/no-anonymous-default-export
export default {
    invertColor
};