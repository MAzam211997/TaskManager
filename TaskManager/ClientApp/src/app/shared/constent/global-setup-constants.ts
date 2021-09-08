var _mode = true;
export const GlobalSetups = {
};

export function GetSetupBool(object: any, setupName: string, defaultValue = false): boolean {

    if (object && object.length > 0) {

        object = object.find(x => x.SetupName == setupName);

        if (object)
            return object.SetupBool;
        else {
            console.log(object);
            console.log(setupName + ": NOT Found");
        }
    }
    return defaultValue;
}

export function GetSetupString(object: any, setupName: string, defaultValue = ''): string {

    if (object && object.length > 0) {

        object = object.find(x => x.SetupName == setupName);

        if (object)
            return object.SetupString;
        else {
            console.log(object);
            console.log(setupName + ": NOT Found");
        }
    }
    return defaultValue;
}

export function GetSetupInt(object: any, setupName: string, defaultValue = 0): number {

    if (object && object.length > 0) {

        object = object.find(x => x.SetupName == setupName);

        if (object)
            return object.SetupInt;
        else {
            console.log(object);
            console.log(setupName + ": NOT Found");
        }
    }
    return defaultValue;
}

export function writeToLog(txt: any) { //mode = false for production
    if (_mode)
    console.log(txt);
}
export function newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
