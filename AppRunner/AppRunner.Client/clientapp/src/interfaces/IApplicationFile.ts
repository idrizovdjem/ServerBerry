export default interface IApplicationFile {
    path: string;
    buffer: ArrayBuffer;
    size: number;
    lastModified: number;
};