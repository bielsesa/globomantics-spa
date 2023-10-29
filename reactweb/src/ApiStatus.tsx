import loading from './loading.gif';

type Args = {
    status: 'idle' | 'success' | 'error' | 'loading';
};

const ApiStatus = ({ status }: Args) => {
    switch (status) {
        case 'error':
            return <div>Error communicating with the data backend</div>;
        case 'idle':
            return <div>Idle</div>;
        case 'loading':
            return (
                <div>
                    <img src={loading} alt='loading  gif' />
                    <p>Loading...</p>
                </div>
            );
        default:
            throw Error('Unknown API state');
    }
};

export default ApiStatus;
