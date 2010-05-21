function SimulateSampling(data, offset, threshold)

%Reset the static variables
ProcessRawData(0, 0, 0, 1);

%If the data is from GlovePie the last row may be incomplete so chop it
%off.
data = data(1:size(data)-1,:);

for n = 2:size(data)
    ProcessRawData(data(n,:), offset, threshold, 0)
end