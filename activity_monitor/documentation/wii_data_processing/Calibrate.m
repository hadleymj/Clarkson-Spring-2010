function result = Calibrate(data, offset)
rows = size(data);
result = [0 0 0];
for i = 1:rows
    result(i,:) = data(i,:) - offset;
end




