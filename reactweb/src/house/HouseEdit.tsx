import { useParams } from 'react-router-dom';
import { useFetchHouse, useUpdateHouse } from '../hooks/houseHooks';
import ApiStatus from '../ApiStatus';
import HouseForm from './HouseForm';
import ValidationSummary from '../ValidationSummary';

const HouseEdit = () => {
    const { id } = useParams();
    if (!id) throw Error('Need a house id.');
    const houseId = parseInt(id);

    const { data, status, isSuccess } = useFetchHouse(houseId);

    // we need to call this hook before returning the ApiStatus comp
    // bc hooks should always be called unconditionally.
    const updateHouseMutation = useUpdateHouse();

    if (!isSuccess) return <ApiStatus status={status} />;

    return (
        <>
            {updateHouseMutation.isError && (
                <ValidationSummary error={updateHouseMutation.error} />
            )}
            <HouseForm
                house={data}
                submitted={(h) => updateHouseMutation.mutate(h)}
            />
        </>
    );
};

export default HouseEdit;
