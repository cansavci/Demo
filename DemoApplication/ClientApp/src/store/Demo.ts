import { Action, Reducer } from 'redux';
import { AppThunkAction } from './index2';

export interface AssetsVariantState {
    isLoading: boolean;
    assets: Asset[]
    folders: Folder[];
    variants: Variant[];
}

export interface Asset {
    id: number;
    name: string;
    folderId: number;
    variants: Variant[];
};

export interface Variant {
    id: number;
    description: string;
}

export interface Folder {
    id: number;
    name: string;
    ownerFolderId?: number;
}

interface RequestDemoAction {
    type: 'REQUEST_Asset';
}

interface ReceiveDemoAction {
    type: 'RECEIVE_Asset';
    assets: Asset[];
}

interface RequestFolderAction {
    type: 'REQUEST_Folder';
}

interface ReceiveFolderAction {
    type: 'RECEIVE_Folder';
    folders: Folder[];
}

interface RequestVariantActionFromAzure {
    type: 'REQUEST_Variant';
}

interface ReceiveVariantActionFromAzure {
    type: 'RECEIVE_Variant';
    variants: Variant[];
}

type KnownAction = RequestDemoAction | ReceiveDemoAction | 
                   RequestFolderAction | ReceiveFolderAction | 
                   RequestVariantActionFromAzure | ReceiveVariantActionFromAzure;

export const actionCreators = {
    requestAssets: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.assets) {
            debugger;
            fetch(`assets`)
                .then(response => response.json() as Promise<Asset[]>)
                .then(data => {
                    debugger;
                    dispatch({ type: 'RECEIVE_Asset', assets: data });
                });
            debugger;
            dispatch({ type: 'REQUEST_Asset' });
        }
    },
    requestFolders: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.assets) {
            fetch(`assets`)
                .then(response => response.json() as Promise<Folder[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_Folder', folders: data });
                });

            dispatch({ type: 'REQUEST_Folder' });
        }
    },
    requestVariantsFromFile: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.assets) {
            fetch(`assets`)
                .then(response => response.json() as Promise<Variant[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_Variant', variants: data });
                });

            dispatch({ type: 'REQUEST_Variant' });
        }
    }
};

const unloadedState: AssetsVariantState = { assets: [], folders: [], variants: [], isLoading: false };

export const reducer: Reducer<AssetsVariantState> = (state: AssetsVariantState | undefined, incomingAction: Action): AssetsVariantState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_Asset':
            return {
                assets: state.assets,
                folders: [],
                variants:[],
                isLoading: true
            };
        case 'RECEIVE_Asset':
            return {
                assets: action.assets,
                folders: [],
                variants:[],
                isLoading: false
            };
        case 'REQUEST_Folder':
            return {
                assets: [],
                folders: state.folders,
                variants:[],
                isLoading: true
            };
        case 'RECEIVE_Folder':
            return {
                assets: [],
                folders: state.folders,
                variants:[],
                isLoading: false
            };
        case 'REQUEST_Variant':
            return {
                assets: [],
                folders: [],
                variants: state.variants,
                isLoading: true
            };
        case 'RECEIVE_Variant':
            return {
                assets: [],
                folders: [],
                variants: state.variants,
                isLoading: false
            };
            break;
    }

    return state;
};
